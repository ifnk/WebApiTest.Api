<template>
  <div>
    <el-breadcrumb separator="/">
      <el-breadcrumb-item :to="{ path: '/home' }">首页</el-breadcrumb-item>
      <el-breadcrumb-item><a href="/">用户管理</a></el-breadcrumb-item>
      <el-breadcrumb-item>用户列表</el-breadcrumb-item>
    </el-breadcrumb>
    <el-dialog
      :title="dialogTitle"
      :visible.sync="userDialogVisible"
      width="60%"
      @close="addUserDialogClosed"
    >
      <el-form ref="userFormRef" :rules="userFormRules" :model="userForm" label-width="100px">
        <el-form-item label="用户名称:" prop="name">
          <el-input v-model="userForm.name" placeholder="请输入用户名称"/>
        </el-form-item>
        <el-form-item label="用户密码:" prop="password">
          <el-input v-model="userForm.password" type="password" placeholder="请输入用户密码"/>
        </el-form-item>
      </el-form>
      <span slot="footer" class="dialog-footer">
        <el-button @click="resetUserFormRef">重置</el-button>
        <el-button type="primary" @click="confirmUserDialogClick">确定</el-button>
      </span>
    </el-dialog>

    <el-card class="box-card">
      <el-row :gutter="20">
        <el-col :span="12">
          <el-input clearable @clear="getUserList" v-model="queryInfo.key" placeholder="请输入用户名"
                    class="input-with-select">
            <el-button @click="getUserList()" slot="append" icon="el-icon-search"/>
          </el-input>
        </el-col>

        <el-col :span="12">
          <el-button @click="registerEvent" type="success">注册事件</el-button>
          <el-button @click="logoutEvent" type="warning">注销事件</el-button>
          <el-button @click="openDialog" type="primary">添加用户</el-button>
        </el-col>
      </el-row>
      <template>
        <el-table
          :data="userList"
          border
          stripe
          style="width: 100%">
          <el-table-column
            type="index"
            label="#"
            width="50">
          </el-table-column>
          <el-table-column
            prop="name"
            label="用户名"
            width="180">
          </el-table-column>
          <el-table-column label="状态">
            <template slot-scope="scope">
              <el-switch
                @change="changeUserStatus(scope.row)"
                v-model="scope.row.status"
                active-color="#13ce66"
              >
              </el-switch>
            </template>
          </el-table-column>
          <el-table-column label="操作">
            <template slot-scope="scope">
              <el-button size="mini" @click="openDialog(scope.row.id)" icon="el-icon-edit" type="primary"/>
              <el-button size="mini" @click="deleteUser(scope.row.id)" icon="el-icon-delete" type="danger"/>
              <el-tooltip :enterable="false" effect="dark" content="分配权限" placement="top-start">
                <el-button size="mini" icon="el-icon-setting" type="warning"/>
              </el-tooltip>
            </template>
          </el-table-column>
        </el-table>
      </template>
      <div class="block pt20">
        <el-pagination
          @size-change="handleSizeChange"
          @current-change="handleCurrentChange"
          :current-page="queryInfo.pageIndex"
          :page-sizes="[2, 5, 10]"
          :page-size="10"
          layout="total, sizes, prev, pager, next, jumper"
          :total="total">
        </el-pagination>
      </div>
    </el-card>

  </div>
</template>

<script>

export default {
  name: 'User',
  data () {
    return {
      dialogTitle: '',
      confirmType: 'add',
      userFormRules: {
        name: [
          { required: true, message: '请输入用户名', trigger: 'blur' },
          { min: 2, max: 20, message: '用户名长度在2-20个字符', trigger: 'blur' }
        ],
        password: [
          { required: true, message: '请输入密码', trigger: 'blur' },
          { min: 2, max: 20, message: '密码长度在2-20个字符', trigger: 'blur' }
        ]
      },

      userForm: {
        name: '',
        password: ''
      },
      userDialogVisible: false,
      // 查询参数
      queryInfo: {
        key: '',
        pageSize: 10,
        pageIndex: 1
      },
      userList: [],
      total: 0
    }
  },
  methods: {

    async registerEvent () {
      const { data: res } = await this.$http.get(`api/coordinate/registerEvente${window.sessionStorage.getItem('userId')}`)
      this.activePath = window.sessionStorage.getItem('activePath')
      this.$message.success(res.msg)
    },
    async logoutEvent () {
      const { data: res } = await this.$http.get(`api/coordinate/logoutEvent`)
      this.$message.success(res.msg)
    },
    async renderEditUserForm (userId) {
      this.confirmType = 'edit'
      const { data: res } = await this.$http.get(`api/users/${userId}`)
      if (!res.success) return this.$message.error(res.msg)
      this.userForm = res.response
      this.userForm.userId = userId
      this.dialogTitle = `编辑用户 : ${res.response.name}`
      this.$message.success(res.msg)
    },
    async renderAddUserForm () {
      this.dialogTitle = '添加用户'
      this.confirmType = 'add'
    },
    async deleteUser (userId) {
      const { data: res } = await this.$http.delete(`api/users/${userId}`)
      if (!res.success) return this.$message.error(res.msg)
      this.getUserList()
      this.$message.success(res.msg)
    },
    async openDialog (e) {
      this.userDialogVisible = true
      return typeof (e) === 'object' ? this.renderAddUserForm() : this.renderEditUserForm(e)
    },
    confirmUserDialogClick () {
      // console.log(this.confirmType )
      // 先对表单进行 预校验
      this.$refs.userFormRef.validate(async valid => {
        if (!valid) return // 没有校验通过
        // 校验通过就 发起添加 的网络请求
        // console.log(this.confirmType)
        const { data: res } = this.confirmType === 'add'
          ? await this.$http.post('api/users', this.userForm)
          : await this.$http.patch(`api/users/${this.userForm.userId}`, this.userForm)
        if (!res.success) return this.$message.error(res.msg)
        this.userDialogVisible = false
        this.getUserList()
        return this.$message.success(res.msg)
      })
    },
    resetUserFormRef () {
      this.$refs.userFormRef.resetFields()
    },
    // 监听 添加 对话框 的 关闭事件
    addUserDialogClosed () {
      this.resetUserFormRef()
    },
    // 监听用户 状态 switch 开关 的改变
    async changeUserStatus (userInfo) {
      const res = await this.$http.put(`api/users/${userInfo.id}/status/${userInfo.status}`)
      if (!res.data.success) {
        userInfo.status = !userInfo.status
        return this.$message.error(res.data.msg)
      }
      return this.$message.success(res.data.msg)
    },
    handleCurrentChange (newPageIndex) { // 监听当前 pageIndex 改变
      this.queryInfo.pageIndex = newPageIndex
      this.getUserList()
    },
    handleSizeChange (newPageSize) { //  监听当前页面 pageSize 的改变
      // console.log(newPageSize)
      this.queryInfo.pageSize = newPageSize
      this.getUserList()
    },
    async getUserList () {
      const res = await this.$http.get(
        'api/users?key=' + this.queryInfo.key + '&pageSize=' + this.queryInfo.pageSize + '&pageIndex=' + this.queryInfo.pageIndex)
      if (!res.data.success) return this.$message.error(res.data.msg)
      // console.log(res.data)
      this.userList = res.data.response.list
      this.total = res.data.response.totalCount
      return this.$message.success(res.data.msg)
    }
  },
  created () {
    this.getUserList()
  }
}
</script>

<style lang="less" scoped>

</style>
