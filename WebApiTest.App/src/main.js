import Vue from 'vue'
import App from './App.vue'
import router from './router'
import './plugins/element.js'
import { showLoading, hideLoading } from './common/loading'
import utils from './common/utils'
import questionHub from './Hub/questionHub'

// 导入字体图标库
import './assets/fonts/iconfont.css'
// 导入全局样式表 (高度占比100% )
import './assets/css/global.css'

import Axios from 'axios'
// 引用公共组件
import PageNation from './components/CommonComponents/PageNation'
Vue.component('PageNation', PageNation)

Vue.prototype.utils = utils
Vue.prototype.$http = Axios
// Vue.use(questionHub)

// 配置请求 的根路径
// Axios.defaults.baseURL = 'http://localhost:5210/'
Axios.defaults.baseURL = '' //  vue 自身 的 proxy 或者  nginx 就 不写这个了

// 提示你当前是 生产环境 还是开发 环境 ，没啥用，就关了
Vue.config.productionTip = false

// 给固定的请求头加上这些
Axios.defaults.headers.post['Content-Type'] = 'application/json; charset=utf-8'

// http request 拦截器 (向后端发送请求前拦截下)
Axios.interceptors.request.use(
  config => {
    showLoading() // 显示加载
    config.headers.authorization = 'Bearer ' + window.sessionStorage.getItem('token')
    return config
  },
  err => {
    return Promise.reject(err)
  }
)

// http response 拦截器 (请求从后端发来的东西后拦截一下)
Axios.interceptors.response.use(
  response => {
    hideLoading() // 隐藏加载
    return response
  },
  error => {
    if (error.toString() === 'Error: Network Error') {
      // Vue.prototype.$message.error('后台服务器没启动，请启动后台服务器！')
      Vue.prototype.$message({
        showClose: true,
        message: '后台服务器没启动，请启动后台服务器！',
        type: 'error',
        duration: 10000 // 这里显示10秒
      })
    }
    if (error.response) {
      console.log(error.response)
      if (error.response.status === 401) {
        // Vue.prototype.$message.error('登录密钥过期，请重新登录！')
        Vue.prototype.$confirm('登录密钥过期或者没有，请重新登录！', '提示', {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning'
        }).then(() => {
          router.push('/login')
        }).catch(() => {
          Vue.prototype.$message({
            type: 'error',
            message: '留在原网页'
          })
        })
      }
      if (error.response.status === 400) {
        Vue.prototype.$message({
          showClose: true,
          message: 'app发送到后台服务器的请求有错误！请检查！',
          type: 'error',
          duration: 10000 // 这里显示10秒
        })
      }
      if (error.response.status === 403) {
        Vue.prototype.$message({
          showClose: true,
          message: '你没有这个操作的权限!',
          type: 'error',
          duration: 10000 // 这里显示10秒
        })
      }
      if (error.response.status === 404) {
        Vue.prototype.$message({
          showClose: true,
          message: '未找到网址或者对应的资源',
          type: 'error',
          duration: 10000 // 这里显示10秒
        })
      }
      if (error.response.status === 405) {
        Vue.prototype.$message({
          showClose: true,
          message: '不被支持的http方法，可能和nginx有关……',
          type: 'error',
          duration: 10000 // 这里显示10秒
        })
      }
      if (error.response.status === 500) {
        Vue.prototype.$message({
          showClose: true,
          message: '500错误!服务器没有启动！',
          type: 'error',
          duration: 10000 // 这里显示10秒
        })
      }
      if (error.response.status === 502) {
        Vue.prototype.$message({
          showClose: true,
          message: '502错误!服务器没有响应!,可能是服务器崩了……',
          type: 'error',
          duration: 10000 // 这里显示10秒
        })
      }
      if (error.response.status === 415) {
        Vue.prototype.$message({
          showClose: true,
          message: '传的参数媒体格式有问题！',
          type: 'error',
          duration: 10000 // 这里显示10秒
        })
      }
      hideLoading() // 隐藏加载
      return '' // 返回接口返回的错误信息
    }
  }
)

new Vue({
  router,
  render: h => h(App)
}).$mount('#app')
