<template>
  <div class="login_container">
    <div class="login_box">
      <!--头像-->
      <div class="avatar_box">
        <img src="../assets/lufw.jpg" alt="">
      </div>
      <!--表单-->
      <el-form :model="loginForm" :rules="loginFormRules" class="login_form" label-width="0px" ref="loginFormRef">
        <el-form-item prop="name">
          <el-input v-model="loginForm.name" prefix-icon="iconfont icon-user" placeholder="请输入用户名"/>
        </el-form-item>
        <el-form-item prop="password">
          <el-input type="password" v-model="loginForm.password" prefix-icon="iconfont icon-3702mima"
                    placeholder="请输入密码"/>
        </el-form-item>
        <el-form-item class="btns">
          <el-button type="primary" @click="login">登录</el-button>
          <el-button type="info" @click="resetLoginForm">重置</el-button>
        </el-form-item>
      </el-form>
    </div>
  </div>
</template>

<script>
export default {
  name: 'Login',
  data () {
    return {
      // 表单的验证规则对象
      loginFormRules: {
        // 验证用户 名
        name: [
          { required: true, message: '请输入用户名!', trigger: 'blur' }
        ],
        // 验证密码
        password: [
          { required: true, message: '请输入密码!', trigger: 'blur' }
        ]
      },
      // 登录 表单 的  数据 绑定对象
      loginForm: {
        name: '',
        password: ''
      }
    }
  },
  methods: {
    login () {
      // 登录前先 校验 下 表单
      this.$refs.loginFormRef.validate(
        async valid => {
          // 验证不通过直接返回
          if (!valid) return
          const res = await this.$http.post('api/users/login', this.loginForm)
          if (!res.data.success) return this.$message.error(res.data.msg)
          this.$message.success(res.data.msg)
          //  将 登录 成功 之后的 token,保存到客户端的 sessionStorage中
          window.sessionStorage.setItem('token', res.data.response.token)
          window.sessionStorage.setItem('userId', res.data.response.user.id)
          //  接着 在 跳转 到首页
          return this.$router.push('/home')
        }
      )
    },
    // 点击重置按钮 ，重置 登录 表单
    resetLoginForm () {
      // 调用 resetFields 方法 将 表单里面 的数据 重置
      this.$refs.loginFormRef.resetFields()
    }

  }

}
</script>

<style lang="less" scoped>

  .login_form {
    position: absolute;
    bottom: 0px;
    width: 100%;
    padding: 0 20px;
    box-sizing: border-box;
    display: flex !important;
    flex-direction: column !important;
    justify-content: center !important;
  }

  .btns {
    display: flex !important;
    flex-direction: column;
    align-items: flex-end !important;
  }

  .avatar_box {
    width: 130px;
    height: 130px;
    border: 1px solid #eee;
    border-radius: 50%;
    display: flex !important;
    align-items: center !important;
    justify-content: center !important;
    box-shadow: 0 0 10px #ddd;
    padding: 5px;
    position: absolute;
    left: 50%;
    transform: translate(-50%, -50%);
    background-color: #eee;

    img {
      width: 100%;
      height: 100%;
      border-radius: 50%;
    }
  }

  .login_box {
    box-shadow: 0 0 10px #ddd;
    position: relative;
    width: 450px;
    height: 300px;
    background-color: #fff;
    border-radius: 10px;
  }

  .login_container {
    background-color: #2b4b6b;
    height: 100%;
    display: flex !important;
    flex-direction: column !important;
    align-items: center !important;
    justify-content: center !important;
  }

</style>
