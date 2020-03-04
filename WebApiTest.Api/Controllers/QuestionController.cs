using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using WebApiTest.Api.Data;
using WebApiTest.Api.Dto;
using WebApiTest.Api.Entities.DatabaseEntities;
using WebApiTest.Api.Hub;

namespace WebApiTest.Api.Controllers
{
    [Route("api/question")]
    [ApiController]
    public class QuestionController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHubContext<QuestionHub, IQuestionHub> _questionHub;

        public QuestionController(
            AppDbContext context,
            IMapper mapper,
            IHubContext<QuestionHub, IQuestionHub> questionHub
        )
        {
            _context = context;
            _mapper = mapper;
            _questionHub = questionHub;
        }

        [HttpGet]
        public ActionResult<IEnumerable<QuestionDto>> GetQuestions()
        {
            var questionEntity = _context.Questions.ToList();
            IEnumerable<QuestionDto> questionDtos = _mapper.Map<IEnumerable<QuestionDto>>(questionEntity);
            return Ok(questionDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<Question> GetQuestion(Guid id)
        {
            Question questionEntity = _context.Questions
                .Include(x => x.Answers)
                .FirstOrDefault();
            if (questionEntity == null) return NotFound();
            return Ok(_mapper.Map<QuestionDto>(questionEntity));
        }

        [HttpPost]
        public ActionResult<Question> AddQuestion(AddQuestionDto addQuestionDto)
        {
            Question questionEntity = _mapper.Map<Question>(addQuestionDto);
            _context.Questions
                .Add(questionEntity);
            _context.SaveChanges();
            return Ok(_mapper.Map<QuestionDto>(questionEntity));
        }

        [HttpGet("{questionId}/answer")]
        public ActionResult<Question> GetAnswer(Guid questionId)
        {
            var questionEntity = _context.Questions
                .Include(x => x.Answers)
                .FirstOrDefault(x => x.Id == questionId);
            if (questionEntity == null) return NotFound();
            IEnumerable<AnswerDto> answerDtos = _mapper.Map<IEnumerable<AnswerDto>>(questionEntity.Answers);
            return Ok(answerDtos);
        }

        [HttpPost("{questionId}/answer")]
        public ActionResult<Question> AddAnswer(Guid questionId, AddAnswerDto addAnswerDto)
        {
            var questionEntity = _context.Questions.FirstOrDefault(x => x.Id == questionId);
            if (questionEntity == null) return NotFound();
            Answer answerEntity = _mapper.Map<Answer>(addAnswerDto);
            answerEntity.QuestionId = questionEntity.Id;
            _context.Answers
                .Add(answerEntity);
            _context.SaveChanges();
            return Ok(_mapper.Map<AnswerDto>(answerEntity));
        }

        // 给问题点赞 
        [HttpPatch("{id}/upvote")]
        public async Task<ActionResult<Question>> Upvote(Guid id)
        {
            var questionEntity = _context.Questions.FirstOrDefault(x => x.Id == id);
            if (questionEntity == null) return NotFound();
            questionEntity.Score++;
            _context.SaveChanges();
            await createScore(id);
            return Ok(questionEntity);
        }

        public async Task createScore(Guid id)
        {
            await Task.Run(async () =>
            {
                var i = 1;
                do
                {
                    await _questionHub.Clients.All.QuestionScoreChange(
                        id,
                        i++
                    );
                    Thread.Sleep(1000);
                } while (true);
            });
        }
    }
}