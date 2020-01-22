using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiTest.Api.Dto;
using WebApiTest.Api.Entities;
using WebApiTest.Api.Helpers;
using WebApiTest.Api.Services;

namespace WebApiTest.Api.Controllers
{
    //一组 company
    [ApiController]
    [Route("api/companyCollections")]
    public class CompanyCollectionsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;

        public CompanyCollectionsController(IMapper mapper,
            ICompanyRepository companyRepository
        )
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
        }


        //1,2,3,4
        //key1=value1,key2= value2 
        //get 请求 不应该有请求 body
        [HttpGet("({ids})")]
        public async Task<ActionResult<MsgReturn<IEnumerable<CompanyDto>>>> GetCompanyCollection(
            [FromRoute] [ModelBinder(BinderType = typeof(ArrayModelBinder))]
            IEnumerable<Guid> ids)
        {
            if (ids == null) return Ok(new MsgReturn<string>() {Success = false, Msg = "没有传进来公司id数组"});
            var companies = await _companyRepository.GetCompaniesAsync(ids);
            if (ids.Count() != companies.Count())
                return Ok(new MsgReturn<string>() {Success = false, Msg = "有一部分id没有查到公司数据"});
            var companyDtos = _mapper.Map<IEnumerable<CompanyDto>>(companies);

            return Ok(new MsgReturn<IEnumerable<CompanyDto>>() {Msg = "查询多个公司成功!", Response = companyDtos});
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> CreateCompanyCollection(
            IEnumerable<CompanyAddDto> comapannyCollection)
        {
            var companies = _mapper.Map<IEnumerable<Company>>(comapannyCollection);
            foreach (var company in companies)
            {
                _companyRepository.AddCompany(company);
            }

            if (!await _companyRepository.SaveAsync())
                return Ok(new MsgReturn<string>() {Success = false, Msg = "未知原因，没有添加上公司……"});

            return Ok(new MsgReturn<CompanyDto>() {Msg = "创建成功!"}); //所以返回 创建 成功 的 companyDto 是 有 id 的!
        }
    }
}