<template>
  <el-container class="home_container">
    <!--头部区域-->
    <el-header>
      <div class="l_header">
        <img src="../assets/lufw.jpg" alt="">
        <span>电商后台管理系统</span>
      </div>
      <el-button @click="logout" type="info">退出</el-button>
    </el-header>

    <!--页面主体-->
    <el-container>
      <!--侧边栏-->
      <el-aside :width="isMenuCollapse ?'64px':'200px'">
        <div class="toggle_button" @click="toggleCollapse">
          <!--折叠图标-->
          |||
        </div>
        <el-menu
          :default-active="activePath"
          router
          :collapse="isMenuCollapse"
          :collapse-transition="false"
          background-color="#3D3D4B"
          text-color="#fff"
          unique-opened
          active-text-color="#409eff"
        >
          <el-submenu index="1">
            <template slot="title">
              <i class="el-icon-user"/>
              <span>用户管理</span>
            </template>
            <el-menu-item @click="saveNavState('user')" index="user">
              <template slot="title">
                <i class="el-icon-menu"/>
                <span>用户列表</span>
              </template>
            </el-menu-item>
          </el-submenu>
          <el-submenu index="2">
            <template slot="title">
              <i class="el-icon-user"/>
              <span>公司管理</span>
            </template>
            <el-menu-item @click="saveNavState('company')" index="company">
              <template slot="title">
                <i class="el-icon-menu"/>
                <span>公司列表</span>
              </template>
            </el-menu-item>
          </el-submenu>
          <el-submenu index="3">
            <template slot="title">
              <i class="el-icon-goods"/>
              <span>商品管理</span>
            </template>
            <el-menu-item @click="saveNavState('store')" index="store">
              <template slot="title">
                <i class="el-icon-menu"/>
                <span>商品列表</span>
              </template>
            </el-menu-item>
          </el-submenu>
        </el-menu>
      </el-aside>
      <!--右侧主体-->
      <el-main>
        <!--路由占位符-->
        <router-view/>
      </el-main>
    </el-container>
  </el-container>

</template>

<script>
export default {
  name: 'Home',
  data () {
    return {
      isMenuCollapse: false,
      activePath: '' // 被激活 的链接地址
    }
  },
  created () {
    this.activePath = window.sessionStorage.getItem('activePath')
  },
  methods: {
    // 保存链接 的 激活 状态
    saveNavState (activePath) {
      window.sessionStorage.setItem('activePath', activePath)
    },
    // 切换 左侧 菜单 的折叠与展开
    toggleCollapse () {
      this.isMenuCollapse = !this.isMenuCollapse
    },
    logout () {
      // 1.清除token
      window.sessionStorage.clear()
      // 2.跳转到登录页面
      this.$router.push('/login')
    }
  }
}
</script>

<style lang="less" scoped>
  .toggle_button {
    font-size: 12px;
    letter-spacing: 0.2em; // 字体间距
    height: 30px;
    color: #eee;
    display: flex !important;
    align-items: center !important;
    justify-content: center !important;
    cursor: pointer; // 鼠标放上去变成小手
  }

  .l_header {
    display: flex !important;
    align-items: center !important;
    justify-content: center !important;
    color: #eee;
    font-size: 20px;

    img {
      width: 50px;
      height: 50px;
      border-radius: 50%;
    }

    span {
      padding-left: 15px;

    }
  }

  .home_container {
    height: 100%;
  }

  .el-header {
    background-color: #373d41;
    display: flex !important;
    align-items: center !important;
    justify-content: space-between !important;
  }

  .el-aside {
    .el-menu {
      border-right: none;
    }

    background-color: #373744;
  }

  .el-main {
    background-color: #EAEDF1;
  }

</style>
