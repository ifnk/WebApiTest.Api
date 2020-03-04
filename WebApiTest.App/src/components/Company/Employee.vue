<template>
  <div>
    <el-breadcrumb separator="/">
      <el-breadcrumb-item :to="{ path: '/home' }">首页</el-breadcrumb-item>
      <el-breadcrumb-item>公司管理</el-breadcrumb-item>
      <el-breadcrumb-item>员工列表</el-breadcrumb-item>
    </el-breadcrumb>
    <el-card class="box-card">
      <div class="df">
        <el-input class="max-w600 flex2" clearable placeholder="请输入员工名">
          <el-button slot="append" icon="el-icon-search"/>
        </el-input>
        <div class="flex1">
          <el-button type="primary" class="ml10 ">添加</el-button>
        </div>
      </div>
      <el-table :data="employeesList" border stripe style="width: 100%">
        <el-table-column type="index" label="#" width="50"/>
        <el-table-column prop="name" label="姓名" width="110"/>
        <el-table-column width="110" label="所在公司" prop="companyName"/>
        <el-table-column width="110" label="年龄" prop="age"/>
        <el-table-column width="110" label="工号" prop="employeeNo"/>
        <el-table-column width="60" label="性别" prop="genderDisplay"/>
        <el-table-column label="操作" prop="employeeNo">
          <template slot-scope="scope">
            <el-button size="mini" icon="el-icon-edit" type="primary"/>
            <el-button size="mini" icon="el-icon-delete" type="danger"/>
          </template>
        </el-table-column>
      </el-table>
      <template>
        <Page-nation :total="queryInfo.total" @pageChange="pageChange"></Page-nation>
      </template>
    </el-card>
  </div>
</template>

<script>
export default {
  name: 'Employee',
  data () {
    return {
      employeesList: [],
      queryInfo: {
        total: 0,
        key: '',
        pageSize: 10,
        pageIndex: 1
      }
    }
  },
  created () {
    this.getAllEmployeeList()
  },
  methods: {
    pageChange (page) {
      this.queryInfo.pageSize = page.pageSize
      this.queryInfo.pageIndex = page.pageIndex
      this.getAllEmployeeList()
    },
    async getAllEmployeeList () {
      const { data: res } = await this.$http.get(`api/companies/employees?key=${this.queryInfo.key}&pageSize=${this.queryInfo.pageSize}&pageIndex=${this.queryInfo.pageIndex}`)
      if (!res.success) return this.$notify.error(res.msg)
      this.employeesList = res.response.list
      this.queryInfo.total = res.response.totalCount
      return this.$notify.success(res.msg)
    }
  }
}
</script>

<style lang="less" scoped>

</style>
