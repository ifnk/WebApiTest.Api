<template>
  <div class=" mt20  bg-blue df jcfs ">
    <el-pagination small class="text-center" @size-change="handleSizeChange" @current-change="handleCurrentChange"
                   :current-page="page.pageIndex" :page-sizes="pageSizes" :page-size="page.pageSize"
                   layout="total, sizes, prev, pager, next, jumper" :total="total">
    </el-pagination>
  </div>
</template>

<script>
export default {
  props: {
    total: {
      type: Number
    } // 总条数
  },
  data () {
    return {
      pageSizes: [5, 10],
      page: {
        pageIndex: 1,
        pageSize: 10
      }
    }
  },
  methods: {
    // // 每页查看条数变化
    handleSizeChange (val) {
      this.page.pageSize = val
      this.$emit('pageChange', this.page)
    },
    // 当前页码变化
    handleCurrentChange (val) {
      this.page.pageIndex = val
      this.$emit('pageChange', this.page)
    }
  }

}
</script>

<style scoped>
</style>
