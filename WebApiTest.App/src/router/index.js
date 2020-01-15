import Vue from 'vue'
import VueRouter from 'vue-router'
import Login from '../components/Login'
import Home from '../components/Home'
import Welcome from '../components/Welcome'
import User from '../components/User/User'
import Store from '../components/Store/Store'
import Company from '../components/Company/Company'

Vue.use(VueRouter)

const routes = [
  // 监听斜线 重定向 到 指定的 路由 (这里是重定向到登录页面 )
  { path: '/', redirect: '/login' },
  { path: '/login', component: Login },
  {
    path: '/home',
    component: Home,
    redirect: '/welcome',
    children: [
      { path: '/welcome', component: Welcome },
      { path: '/user', component: User },
      { path: '/store', component: Store },
      { path: '/company', component: Company }
    ]
  }

]

const router = new VueRouter({
  routes
})
// 挂 载 路由 导航 首卫   检查你 有没有token 没有的话就 强制 他跳转 到 登录 页面
router.beforeEach((to, form, next) => {
// to 将要 访问的路径
// from 代表从哪个路径 跳转
// next  是一个函数 表示 放行
  if (to.path === '/login') return next() // 如果任何用户 要访问 登录页面 就 随便放行
  // 获取token
  const tokenStr = window.sessionStorage.getItem('token')
  if (!tokenStr) return next('/login') // 如果token 不存在 就让他 强制 跳转 到登录 页面
  // 存在 token 的话 就 放行啦
  next()
})

export default router
