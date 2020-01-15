<template>
  <div>
    <el-breadcrumb separator="/">
      <el-breadcrumb-item :to="{ path: '/home' }">首页</el-breadcrumb-item>
      <el-breadcrumb-item><a href="/">用户管理</a></el-breadcrumb-item>
      <el-breadcrumb-item>用户列表</el-breadcrumb-item>
    </el-breadcrumb>

    <el-card class="box-card">

      <el-row :gutter="20">
        <el-col :span="12">
          <el-input placeholder="请输入用户名" class="input-with-select">
            <el-button slot="append" icon="el-icon-search"/>
          </el-input>
        </el-col>
        <el-col :span="4">
          <el-button type="primary">添加用户</el-button>
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
                v-model="scope.row.status"
                active-color="#13ce66"
              >
              </el-switch>
            </template>
          </el-table-column>
          <el-table-column label="操作">
            <template slot-scope="scope">
              <el-button size="mini" icon="el-icon-edit" type="primary"/>
              <el-button size="mini" icon="el-icon-delete" type="danger"/>
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
          :page-size="2"
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
    handleCurrentChange () { // 监听当前 pageIndex 改变

    },
    handleSizeChange (newPageSize) { //  监听当前页面 pageSize 的改变
      console.log(newPageSize)
      this.queryInfo.pageSize = newPageSize
      this.getUserList()
    },
    async getUserList () {
      const res = await this.$http.get(
        'api/users?key=' + this.queryInfo.key + '&pageSize=' + this.queryInfo.pageSize + '&pageIndex=' + this.queryInfo.pageIndex)
      if (!res.data.success) return this.$message.error(res.data.msg)
      console.log(res.data)
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
