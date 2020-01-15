import Vue from 'vue'
import {
  Aside,
  Link,
  Button,
  Breadcrumb,
  Pagination,
  BreadcrumbItem,
  Container,
  Header,
  Main,
  Form,
  Menu,
  Submenu,
  MenuItem,
  MenuItemGroup,
  FormItem,
  Input,
  Option,
  Select,
  Card,
  Switch,
  Tooltip,
  Row,
  Col,
  Table,
  TableColumn,
  Dialog,
  MessageBox,
  Message,
  Loading
} from 'element-ui'

Vue.use(Button)
Vue.use(Dialog)
Vue.use(Link)
Vue.use(Form)
Vue.use(FormItem)
Vue.use(Input)
Vue.use(Container)
Vue.use(Header)
Vue.use(Pagination)
Vue.use(Aside)
Vue.use(Switch)
Vue.use(Main)
Vue.use(Menu)
Vue.use(Submenu)
Vue.use(MenuItem)
Vue.use(MenuItemGroup)
Vue.use(Breadcrumb)
Vue.use(Tooltip)
Vue.use(BreadcrumbItem)
Vue.use(Card)
Vue.use(Option)
Vue.use(Select)
Vue.use(Row)
Vue.use(Col)
Vue.use(Table)
Vue.use(TableColumn)

// 这个 是 script 里面的方法 所以要全局挂载到 vue (this里面)
Vue.prototype.$elLoading = Loading
Vue.prototype.$message = Message
Vue.prototype.$confirm = MessageBox.confirm
