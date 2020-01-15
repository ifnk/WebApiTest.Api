<template>
  <div>
    <el-breadcrumb separator="/">
      <el-breadcrumb-item :to="{ path: '/home' }">首页</el-breadcrumb-item>
      <el-breadcrumb-item><a href="/">公司管理</a></el-breadcrumb-item>
      <el-breadcrumb-item>公司列表</el-breadcrumb-item>
    </el-breadcrumb>
    <el-row :gutter="20">
      <el-col :span="12">
        <el-input placeholder="请输入公司名" class="input-with-select">
          <el-button slot="append" icon="el-icon-search"/>
        </el-input>
      </el-col>
      <el-col :span="4">
        <el-button type="primary">添加公司</el-button>
      </el-col>
    </el-row>

    <!--点击公司详情(员工列表)模态框 -->
    <el-dialog :title="employeeDialogInfo.title+' 公司的员工列表'"
               :visible.sync="employeeDialogInfo.visable"
               width="80%">
      <el-table :data="employeeDialogInfo.employeeList" stripe border style="width: 100%">
        <el-table-column prop="date" label="#" type="index" width="50"></el-table-column>
        <el-table-column label="姓名" prop="name" width="100"/>
        <el-table-column label="性别" prop="genderDisplay" width="100"/>
        <el-table-column label="年龄" prop="age" width="100"/>
        <el-table-column label="工号" prop="employeeNo" width="100"/>
        <el-table-column prop="address" label="操作">
          <template slot-scope="scope">
            <el-button size="mini" icon="el-icon-edit" type="primary"/>
            <el-button size="mini" icon="el-icon-delete" type="danger"/>
          </template>
        </el-table-column>
      </el-table>


    </el-dialog>
    <el-card class="box-card mt15">
      <el-table :data="companyList" stripe border style="width: 100%">
        <el-table-column
          prop="date"
          label="#"
          type="index"
          width="50">
        </el-table-column>
        <el-table-column label="公司名称" width="180">
          <template slot-scope="scope">
            <el-tooltip class="item" :enterable="false" effect="dark" content="查看员工列表" placement="top-start">
              <el-link @click="viewEmployeeList(scope.row.id,scope.row.companyName)" type="primary">
                {{scope.row.companyName}}
              </el-link>
            </el-tooltip>
          </template>
        </el-table-column>
        <el-table-column prop="address" label="操作">
          <template slot-scope="scope">
            <el-button size="mini" icon="el-icon-edit" type="primary"/>
            <el-button size="mini" icon="el-icon-delete" type="danger"/>
          </template>
        </el-table-column>
      </el-table>
    </el-card>
  </div>
</template>

<script>
export default {
  name: 'Company',
  data () {
    return {
      // 查询参数
      employeeDialogInfo: {
        // visable: false,
        visable: false,
        employeeList: [],
        title: ''
      },
      queryInfo: {
        key: '',
        pageSize: 10,
        pageIndex: 1
      },
      companyList: [],
      total: 0
    }
  },
  methods: {
    viewEmployeeList (companyId, companyName) {
      // console.log(companyId + ',' + companyName)
      this.employeeDialogInfo.visable = true
      this.employeeDialogInfo.title = companyName
      this.getEmployeeList(companyId)
    },
    async getEmployeeList (companyId) {
      const res = await this.$http.get('api/companies/' + companyId + '/employees')
      if (!res.data.success) return this.$message.error(res.data.msg)
      this.employeeDialogInfo.employeeList = res.data.response.list
      // console.log(this.employeeDialogInfo.employeeList)
      return this.$message.success(res.data.msg)
    },
    async getCompanyList () {
      const res = await this.$http.get('api/companies')
      if (!res.data.success) return this.$message.error(res.data.msg)
      // console.log(res.data.response.list)
      this.companyList = res.data.response.list
      return this.$message.success(res.data.msg)
    }
  },
  created () {
    this.getCompanyList()
  }
}
</script>

<style lang="less" scoped>
</style>
