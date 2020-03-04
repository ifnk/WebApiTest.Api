module.exports = {
  lintOnSave: false, // 使用带有浏览器内编译器的完整构建版本 // https://vuejs.org/v2/guide/installation.html#Runtime-Compiler-vs-Runtime-only
  devServer: { // 部署相关
    open: true,
    host: '127.0.0.1',
    port: 5211, // 当前 vue 端口 号
    https: false,
    hotOnly: false, // https:{type:booblean}
    proxy: { // 本地 nodejs 自带 的一个插件
      // 配置多个代理  这样一个vue 项目 就可以对应 多个项目 了
      '/api': { // 如果请求 的是  api 为开头 的 ，就会找 这个  http://localhost:8080 端口号
        // /api/login ==> localhost:8080/api/login
        // /apb/login ==> localhost:2364/api/login
        target: 'https://localhost:5210', // 这里改成你自己的后端api 端口地址 ，记得 每次修改，都需要重新 build
        ws: true,
        changeOrigin: true,
        pathRewrite: {
          '^/apb': '' // 替换 target中的请求 地址
        }
      }
    }

  }
}
