import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr'

export default {
  install (Vue) {
    const questionHub = new Vue()
    Vue.prototype.$queestionHub = questionHub
    const connection = new HubConnectionBuilder()
      .withUrl('api/question-hub') // 注意这里不能写 http://localhost:5210/api/question-hub 卡在这里好旧了,不然会报跨域的错!
      .configureLogging(LogLevel.Information)
      .build()
    let startedPromise = null

    connection.on('QuestionScoreChange', (questionId, score) => {
      questionHub.$emit('score-changed', { questionId, score })
    })

    function start () { // 定义 一下start  让他可以 断线重连并且提示
      startedPromise = connection.start().catch(err => {
        Vue.prototype.$message({
          showClose: true,
          message: `连接服务器的signalR 失败 ! ${err}`,
          type: 'error',
          duration: 10000 // 这里显示10秒
        })
        return new Promise((resolve, reject) =>
          setTimeout(() => start().then(resolve).catch(reject), 5000))
      })
      return startedPromise
    }

    connection.onclose(() => start())
    start()
  }
}
