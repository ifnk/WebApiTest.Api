<template>
  <div>
    <el-breadcrumb separator="/">
      <el-breadcrumb-item :to="{ path: '/home' }">首页</el-breadcrumb-item>
      <el-breadcrumb-item><a href="/">公司管理</a></el-breadcrumb-item>
      <el-breadcrumb-item>公司列表</el-breadcrumb-item>
    </el-breadcrumb>
    <el-row :gutter="20">
      <el-col :span="12">
        <el-input clearable placeholder="请输入公司名" v-model="queryInfo.key" class="input-with-select">
          <el-button @click="getCompanyList()" slot="append" icon="el-icon-search"/>
        </el-input>
      </el-col>
      <el-col :span="4">
        <el-button @click="addCompanyClick" type="primary">添加公司</el-button>
      </el-col>
      <el-col :span="4">
        <el-button @click="test" type="primary">测试</el-button>
      </el-col>
    </el-row>

    <!--添加公司-->
    <el-dialog title="添加公司"
               :visible.sync="addCompanyVisible"
               custom-class="addDialog"
               :close-on-click-modal="false"
               width="80%">
      <el-form :model="addCompanyForm" label-width="80px">
        <el-form-item label="公司名称:" prop="companyName">
          <el-input placeholder="请输入公司名称"/>
        </el-form-item>
      </el-form>

      <template slot="footer">
        <el-button type="primary">添加</el-button>
        <el-button type="info">重置</el-button>
      </template>
    </el-dialog>
    <!--点击公司详情(员工列表)模态框 -->
    <el-dialog :title="employeeDialogInfo.title+' 公司的员工列表'"
               :visible.sync="employeeDialogInfo.visible"
               :close-on-click-modal="false"
               custom-class="companyDetailDialog"
               width="80%">
      <el-table :data="employeeDialogInfo.employeeList" stripe border style="width: 100%">
        <el-table-column prop="date" label="#" type="index" width="50"/>
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
      addCompanyForm: {
        companyName: ''
      },
      addCompanyVisible: false,
      // 查询参数
      employeeDialogInfo: {
        // visable: false,
        visible: false,
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
  watch: {
    queryInfo: { // 监听key 的实时变化 返回 搜索结果
      handler (queryInfo) {
        this.getCompanyList()
      },
      deep: true
    }
  },
  methods: {
    addCompanyClick () {
      this.addCompanyVisible = true
    },
    viewEmployeeList (companyId, companyName) {
      // console.log(companyId + ',' + companyName)
      this.employeeDialogInfo.visible = true
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
      // console.log(this.queryInfo.key)
      const res = await this.$http.get(`api/companies?key=${this.queryInfo.key}&pageSize=${this.queryInfo.pageSize}&pageIndex=${this.queryInfo.pageIndex}`)
      if (!res.data.success) return this.$notify.error(res.data.msg)
      // console.log(res.data.response)
      this.companyList = res.data.response.list
      return this.$notify.success(res.data.msg)
    }
  },
  created () {
    this.getCompanyList()
  }
}
</script>

<style lang="less">
  .addDialog {
    .el-dialog__body {
      padding: 10px 20px;

      .el-form-item {
        margin-bottom: 10px;
      }
    }

    .el-dialog__footer {
      padding: 0 20px 10px 20px;
    }
  }

  .companyDetailDialog {
    .el-dialog__body {
      padding-top: 0;
    }
  }

</style>
