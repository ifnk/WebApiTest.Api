<template>
  <div>
    <el-card :class="index===0?'':'mt20' " v-for="(item,index) in questionList" :key="index">
      <div class="df  ">
        <Score :question="item"/>
        <div class="dfc  jcsb">
          <div class="fz20">
            {{item.title}}
          </div>
          <div class="fz15">
            {{item.body}}
          </div>
          <el-link type="primary" :underline="false" class="fz13 w55" @click="openQuestionDetails(item.id)">
            查看问题
          </el-link>
        </div>
      </div>
    </el-card>
    <el-dialog
      title="问题详情"
      :visible.sync="questionDialogVisible"
      width="90%"
    >
      <div class="df  ">
        <div class="w80 pr10 jcc aic dfc ">
          <div>
            <i class="el-icon-caret-top fz20 " style="color: dodgerblue;"></i>
          </div>
          <div class=" fz27">
            {{question.score}}
          </div>
          <div>
            <i class="el-icon-caret-bottom fz20" style="color: dodgerblue;"></i>
          </div>
        </div>
        <div class="dfc  jcsb">
          <div class="fz20">
            {{question.title}}
          </div>
          <div class="fz15">
            {{question.body}}
          </div>
        </div>
      </div>
      <div class="df   ">
        <div class="w80 pr10 jcc aic dfc "></div>
        <div class="dfc  jcsb p10" style=" width: 100%;">
          <el-divider></el-divider>
          <div v-for="(answer,index) in answerList" :key="index">
            {{answer.body}}
            <el-divider></el-divider>
          </div>
        </div>
      </div>

    </el-dialog>
  </div>
</template>

<script>

import Score from './Score'

export default {
  name: 'Question',
  components: { Score },
  data () {
    return {
      questionList: [],
      answerList: [],
      question: {},
      questionDialogVisible: false
    }
  },
  watch: {},
  methods: {
    async getQuestionList () {
      const { data: res } = await this.$http.get('api/question')
      this.questionList = res
    },
    async openQuestionDetails (questionId) {
      console.log(questionId)
      const { data: question } = await this.$http.get(`api/question/${questionId}`)
      const { data: answerList } = await this.$http.get(`api/question/${questionId}/answer`)
      this.question = question
      this.answerList = answerList
      this.questionDialogVisible = true
    }
  },
  created () {
    this.getQuestionList()
    // 1、首先我们实例化一个连接器
    console.log(123)
    // const connection = new signalR.HubConnectionBuilder() // 连接 signalR
    //   // 然后配置通道路由
    //   .withUrl('http://localhost:5210//question-hub')
    //   // 日志信息
    //   .configureLogging(signalR.LogLevel.Information)
    //   // 创建
    //   .build()
    // connection.start()
  }
}
</script>

