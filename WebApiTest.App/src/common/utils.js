export default {
  pageIndex: 30,
  changePageIndex (newPageIndex) { // 监听当前 pageIndex 改变
    console.log(newPageIndex)
    // page.queryInfo.pageIndex = newPageIndex
    // page.getUserList()
  },
  text (queryInfo) {
    // console.log(event)
  }
}
