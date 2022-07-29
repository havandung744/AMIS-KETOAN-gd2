<template>
  <div class="d-employee-box">
    <div class="d-employee-box-info">
      <div class="header">
        <div class="header-text">THÔNG TIN NHÂN VIÊN</div>
        <div class="header-content">
          <div class="header-checkbox">
            <input type="checkbox" id="check1" @click="selectOnlyCheckbox($event)" style="width: 18px; height: 18px" />
            <div class="header-checkbox-content"
            >Là khách hàng</div>
          </div>
          <div class="header-checkbox">
            <input
              type="checkbox"
               id="check2"
              style="width: 18px; height: 18px"           
              @change="checkSupplier($event)"
              @click="selectOnlyCheckbox($event)"
            />
            <div class="header-checkbox-content">Là nhà cung cấp</div>
          </div>
        </div>
        <div class="icon_close header-close" @click="btnCloseOnClick"></div>
      </div>
      <div class="content">
        <div class="content-info">
          <div class="content-info-left">
            <div class="info-input-tr">
              <div class="info-input">
                <label for=""
                  >Mã nhân viên&nbsp;<span style="color: red">*</span></label
                ><br />
                <input
                  type="text"
                  v-model="employee.EmployeeCode"
                  class="d-input"
                  id="EmployeeCode"
                  @blur="validateCheckInput"
                  tabindex="1"
                  style="width: 150px"
                />
              </div>
              <div class="info-input">
                <label for=""
                  >Họ tên&nbsp;<span style="color: red">*</span></label
                ><br />
                <input
                  type="text"
                  v-model="employee.EmployeeName"
                  class="d-input"
                  id="EmployeeName"
                  @blur="validateCheckInput"
                  tabindex="2"
                  style="width: 235px"
                />
              </div>
            </div>
            <div class="info-input-tr">
              <div class="info-input">
                <label for="">Đơn vị</label><br />

                <select
                  tabindex="7"
                  v-model="employee.DepartmentId"
                  class="cbxDepartment"
                >
                  <option value="142cb08f-7c31-21fa-8e90-67245e8b283e">
                    Phòng đào tạo
                  </option>
                  <option value="17120d02-6ab5-3e43-18cb-66948daf6128">
                    Phòng nhân sự
                  </option>
                  <option value="469b3ece-744a-45d5-957d-e8c757976496">
                    Phòng sản xuất
                  </option>
                  <option value="4e272fc4-7875-78d6-7d32-6a1673ffca7c">
                    Phòng kế toán
                  </option>
                </select>
                <!-- 
                <el-select v-model="employee.DepartmentId" filterable placeholder="Select">
    <el-option
      v-for="item in options"
      :key="item.value"
      :label="item.label"
      :value="item.value"
    />
  </el-select> -->
              </div>
            </div>
            <div class="info-input-tr">
              <div class="info-input">
                <label for="">Chức danh&nbsp;<span style="color: red">*</span></label><br />
                <input
                  type="input"
                  class="d-input"
                  id="EmployeePosition"
                  @blur="validateCheckInput"
                  v-model="employee.EmployeePosition"
                  tabindex="10"
                  style="width: 391px"
                />
              </div>
            </div>
          </div>
          <div class="content-info-right">
            <div class="info-input-tr">
              <div class="info-input">
                <label for="">Ngày sinh</label><br />
                <!-- <input type="date" id="date_timepicker_end" v-model="employee.DateOfBirth" class="d-input" tabindex="3"
                  style="width: 161px" /> -->
                <el-date-picker
                  id="date_timepicker_end"
                  tabindex="3"
                  v-model="employee.DateOfBirth"
                  type="date"
                  placeholder="DD/MM/YYYY"
                  format="DD/MM/YYYY"
                  value-format="YYYY-MM-DD"
                  style="
                    width: 161px;
                    height: 32px;
                    padding-right: 12px;
                    margin-top: 4px;
                    font-family: GoogleSans;
                    font-size: 13px;
                  "
                />
              </div>
              <div class="info-input" id="d-radio">
                <label for="" style="margin-left: 4px">Giới tính</label><br />
                <div class="info-input-box">
                  <div class="info-input-item">
                    <input
                      name="gender"
                      type="radio"
                      value="1"
                      tabindex="4"
                      v-model="employee.Gender"
                    />
                    <label for="" style="margin-right: 8px">Nam</label>
                  </div>
                  <div class="info-input-item">
                    <input
                      name="gender"
                      type="radio"
                      value="0"
                      tabindex="5"
                      v-model="employee.Gender"
                    />
                    <label for="" style="margin-right: 8px">Nữ</label>
                  </div>
                  <div class="info-input-item">
                    <input
                      name="gender"
                      type="radio"
                      value="2"
                      tabindex="6"
                      v-model="employee.Gender"
                    />
                    <label for="">Khác</label>
                  </div>
                </div>
              </div>
            </div>
            <div class="info-input-tr">
              <div class="info-input">
                <label for="">Số CMND</label><br />
                <input
                  type="input"
                  id="IdentityNumber"
                  v-model="employee.IdentityNumber"
                  @blur="validateCheckIdentityNumber"
                  class="d-input"
                  tabindex="8"
                  style="width: 245px"
                />
              </div>
              <div class="info-input">
                <label for="">Ngày cấp</label><br />
                <!-- <input type="date" id="date_time_IdentityDate" v-model="employee.IdentityDate" class="d-input"
                  tabindex="9" style="width: 140px" /> -->
                <el-date-picker
                  id="date_time_IdentityDate"
                  tabindex="9"
                  v-model="employee.IdentityDate"
                  type="date"
                  placeholder="DD/MM/YYYY"
                  format="DD/MM/YYYY"
                  value-format="YYYY-MM-DD"
                  style="
                    width: 152px;
                    height: 32px;
                    padding-right: 12px;
                    margin-top: 4px;
                    font-family: GoogleSans;
                    font-size: 13px;
                  "
                />
              </div>
            </div>
            <div class="info-input-tr">
              <div class="info-input">
                <label for="">Nơi cấp</label><br />
                <input
                  type="input"
                  v-model="employee.IdentityPlace"
                  class="d-input"
                  tabindex="11"
                  style="width: 391px"
                />
              </div>
            </div>
          </div>
        </div>
        <div class="info-input-tr" v-show="this.isShowSupplier" id="isShowSupplier">
            <div class="info-input">
              <label for="">Tên tổ chức&nbsp;<span style="color: red">*</span></label><br />
              <input
                type="text"
                class="d-input"
                id="EmployeeCode"
                v-model="employee.OrganizationName"
                 @blur="validateCheckInput"
                tabindex="16"
                style="width: 150px"
              />
            </div>
            <div class="info-input">
              <label for="">Mã số thuế&nbsp;<span style="color: red">*</span></label><br />
              <input
                type="text"
                class="d-input"
                id="EmployeeName"
                v-model="employee.TaxCode"
                 @blur="validateCheckInput"
                tabindex="17"
                style="width: 235px"
              />
            </div>
            <div class="info-input">
              <label for="">Địa chỉ tổ chức&nbsp;<span style="color: red">*</span></label><br />
              <input
                type="text"
                class="d-input"
                id="EmployeeName"
                v-model="employee.OrganizationAddress"
                 @blur="validateCheckInput"
                tabindex="18"
                style="width: 235px"
              />
            </div>
          </div>
        <div class="info-input-tr" style="margin-top: 40px">
          <div class="info-input">
            <label for="">Địa chỉ</label>
            <input
              type="input"
              class="d-input"
              v-model="employee.Address"
              tabindex="12"
              style="width: 812px"
            />
          </div>
        </div>
        <div class="content-info-bottom">
          <div class="info-input-tr">
            <div class="info-input">
              <label for="">ĐT di động</label><br />
              <input
                type="text"
                v-model="employee.PhoneNumber"
                class="d-input"
                id="EmployeeCode"
                tabindex="13"
                style="width: 150px"
              />
            </div>
            <div class="info-input">
              <label for="">ĐT cố định</label><br />
              <input
                type="text"
                class="d-input"
                id="EmployeeName"
                v-model="employee.TelephoneNumber"
                tabindex="14"
                style="width: 235px"
              />
            </div>
            <div class="info-input">
              <label for="">Email</label><br />
              <input
                type="text"
                v-model="employee.Email"
                class="d-input"
                id="EmployeeName"
                tabindex="15"
                @blur="validateCheckEmail"
                style="width: 235px"
              />
            </div>
          </div>

          <div class="info-input-tr">
            <div class="info-input">
              <label for="">Tài khoản ngân hàng</label><br />
              <input
                type="text"
                class="d-input"
                id="EmployeeCode"
                v-model="employee.BankAccountNumber"
                tabindex="16"
                style="width: 150px"
              />
            </div>
            <div class="info-input">
              <label for="">Tên ngân hàng</label><br />
              <input
                type="text"
                class="d-input"
                id="EmployeeName"
                v-model="employee.BankName"
                tabindex="17"
                style="width: 235px"
              />
            </div>
            <div class="info-input">
              <label for="">Chi nhánh</label><br />
              <input
                type="text"
                class="d-input"
                id="EmployeeName"
                v-model="employee.BankBranchName"
                tabindex="18"
                style="width: 235px"
              />
            </div>
          </div>
        </div>
        <div class="footer">
          <div class="footer-line"></div>
          <div class="footer-button">
            <button
              class="d-btn d-btn-cancel d-btn-footer-left"
              id="d-btn-close"
              tabindex="21"
              @blur="focusInputFirst"
              @click="btnCloseOnClick"
            >
              Hủy
            </button>
            <button
              class="d-btn d-btn-save"
              id="d-btn-save"
              tabindex="20"
              @click="btnSaveOnClick"
            >
              Cất
            </button>
            <button
              class="d-btn d-btn-save-add"
              id="d-btn-save-add"
              tabindex="19"
              @click="btnSaveAndAdd"
            >
              Cất và Thêm
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import axios from "axios";
import { miSaResource } from "../../js/miSaResource";
// import {vi} from 'date-fns/locale';

// import Datepicker from "../common/DatepickerList.vue";
export default {
  name: "EmployeeDetail",
  components: {
    //  Datepicker
  },
  props: [
    "employeeIdSelected",
    "formMode",
    "pageSize",
    "pageNumberSelected",
    "textSearch",
    "bankName",
    "gender",
    "departmentId",
    "checkOrganization",
    "checkReplication",
  ],
  data() {
    return {
      // dateTime: new Date(2016, 0, 1),
      // dateTime: new Date(employee.date),
      employee: {},
      employees: {},
      isShowSupplier: false,
    };
  },
  watch: {
    /**
     * kiểm tra sự thay đổi của employeeIdSelected
     * khi có sự thay đổi thì sẽ lấy thông tin của nhân viên và đưa lên form detail
     * đồng thời kiểm tra xem có phải là chế độ nhân bản hay không (nếu là chế độ nhân bản thì sẽ lấy lại toàn bộ thông tin đồng thòi
     * lấy mã nhân viên mới)
     * @param {object} value là id của đối tượng nhân được khi click
     * Author: HVDUNG(05/06/2022)
     */
    employeeIdSelected: async function (value) {
      await this.getEmployeeInfo(value);
      if (this.checkReplication) {
        await this.getNewEmployeeCode();
      }
    },
  },

  methods: {

    /**
     * Thực hiện focus vào ô checkbox này thì ô checkbox kia không được focus nữa
     * thông tin ẩn hiện theo checkbox lựa chọn
     * @param {*} event 
     * Author: HVDUNG(29/07/2022) 
     */
    selectOnlyCheckbox(event){
      if(event.target.id==="check1" && document.getElementById("check1").checked===true){
        document.getElementById("check2").checked=false;
        this.isShowSupplier = false;
        this.employee.IsOrganizations = false;
      }
      if(event.target.id==="check2" && document.getElementById("check2").checked===true){
        document.getElementById("check1").checked=false;
      }
    },

     /**
     * Thực hiện kiểm tra có checked checkbox là nhà cung cấp hay không
     * show thêm trường thông tin khi click checkbox này
     * @param {*} event 
     * Author: HVDUNG (19/07/2022)
     */
    checkSupplier(event) {
      if (event.currentTarget.checked) {
        this.isShowSupplier = true;
        this.employee.IsOrganizations = true;
      } else {
        this.isShowSupplier = false;
        this.employee.IsOrganizations = false;
      }
    },

    /**
     * Thực hiện lấy dữ liệu từ cliend và trả về cho form chi tiết
     * @param {object} value là employeeId nhân được khi click
     * Author: HVDUNG(05/06/2022)
     */
    async getEmployeeInfo(value) {
      // kiểm tra xem id có tồn tại hay không
      var me = this;
      // id tồn tại -> show thông tin tương ứng với id này
      if (value) {
        await axios
          .get(
            `http://localhost:22454/api/v1/Employees/${me.employeeIdSelected}`
          )
          .then(function (res) {
            switch (res.status) {
              case 200:
                // thực hiện focus vào ô nhập liệu đầu tiên
                document.getElementById("EmployeeCode").focus();
                // gán lại data cho employee detail
                me.employee = res.data;
                break;
              case 201:
                break;
              default:
                break;
            }
          })
          .catch(function (res) {
            console.log(res);
          });
      }
      // không có id -> thực hiện thêm mới
      else {
        //gán lại employee là rỗng khi thêm mới
        me.employee = {};
        // gán mặc định giới tính là nam
        me.employee.Gender = 1;
        // lấy mã nhân viên mới
        await me.getNewEmployeeCode();
      }
    },

    /**
     * Thực hiện lưu khi người dùng click nút lưu
     * Author: HVDUNG (02/06/2022)
     */
    btnSaveOnClick() {
      var me = this;
      //1. validate dữ liệu
      var isValid = me.validate();
      //2. thực hiện thêm mới hoặc cập nhật khi validate thành công
      if (isValid) {
        //2.1 nếu formMode là 1 thì thêm mới
        if (me.formMode == 1) me.addEmployee();
        //2.2 nếu formMode != 1 thì cập nhật
        else me.updateEmployee();
        //3. thực hiện đóng form chi tiết
        me.$emit("isShowDialog");
      }
    },

    /**
     * Thực hiện lưu khi người dùng click nút lưu đồng thời reset value cho form detail
     * Author: HVDUNG (02/06/2022)
     */
    async btnSaveAndAdd() {
      var me = this;
      //1. thực hiện việc validate
      var isValid = me.validate();
      //2. thực hiện thêm mới hoặc cập nhật khi validate thành công
      if (isValid) {
        //2.1 nếu formMode là 1 thì thêm mới
        if (me.formMode == 1) {
          await me.addEmployee();
          me.employee = {};
          await me.getNewEmployeeCode();
        }
        //2.2 nếu formMode != 1 thì cập nhật
        else {
          await me.updateEmployee();
          await me.$emit("changeValueFormMode", 1);
          me.employee = {};
          await me.getNewEmployeeCode();
        }
      }
    },

    /**
     * Thực hiện đóng form chi tiết khi click
     * Author: HVDUNG(05/06/2022)
     */
    btnCloseOnClick() {
      //Thực hiện đống form chi tiết
      this.$emit("isShowDialog");
    },

    /**
     * thực hiện focus vào ô nhập liệu đầu tiên khi nó đến input cuối cùng
     * Author: HVDUNG (03/06/2022)
     */
    focusInputFirst() {
      document.getElementById("EmployeeCode").focus();
    },

    /**
     * thực hiện validate xem dữ liệu có bị trống hay không
     * Author: HVDUNG (02/06/2022)
     */
    validateCheckInput(event) {
      //theo cách dùng thuần
      //1. lấy ra giá trị input nhập vào
      let value = event.currentTarget.value;
      //  2. kiểm tra value có rỗng hay không
      if (!value) {
        // thêm class vào trong input
        event.currentTarget.classList.add("d-input-error");
        // thêm câu cảnh báo khi hover
        event.currentTarget.setAttribute("title", miSaResource.VI.infoNotNull);
      } else {
        event.currentTarget.classList.remove("d-input-error");
        event.currentTarget.setAttribute("title", "");
      }
    },

    /**
     * Thực hiện kiểm tra xem email đã đúng định dạng hay chưa
     * @param {event} event đối tượng được xây dựng sẵn trong Vue
     * Author: HVDUNG (11/06/2022)
     */
    validateCheckEmail(event) {
      //theo cách dùng thuần
      //1. lấy ra giá trị input nhập vào
      let value = event.currentTarget.value;
      //  2. kiểm tra value có đụng định dạng hay không
      if (value.trim() != "" && !this.checkEmail(value)) {
        // thêm class vào trong input
        event.currentTarget.classList.add("d-input-error");
        // thêm câu cảnh báo khi hover
        event.currentTarget.setAttribute(
          "title",
          miSaResource.VI.InvalidEmailFormat
        );
      } else {
        event.currentTarget.classList.remove("d-input-error");
        event.currentTarget.setAttribute("title", "");
      }
    },

    /**
     * Thực hiện kiểm tra xem email đã đúng định dạng hay chưa
     * @param {event} event đối tượng được xây dựng sẵn trong Vue
     * Author: HVDUNG (11/06/2022)
     */
    validateCheckIdentityNumber(event) {
      //theo cách dùng thuần
      //1. lấy ra giá trị input nhập vào
      let value = event.currentTarget.value;
      //  2. kiểm tra value có đụng định dạng hay không
      if (value.trim() != "" && !this.checkIdentityNumber(value)) {
        // thêm class vào trong input
        event.currentTarget.classList.add("d-input-error");
        // thêm câu cảnh báo khi hover
        event.currentTarget.setAttribute(
          "title",
          miSaResource.VI.InvalidIdentityFormat
        );
      } else {
        event.currentTarget.classList.remove("d-input-error");
        event.currentTarget.setAttribute("title", "");
      }
    },

    /**
     * Thực hiện kiểm tra email có đúng định dạng hay không
     * @param {string} email email cuả nhân viên
     * Author: HVDUNG (11/06/2022)
     */
    checkEmail(email) {
      var res = /\S+@\S+\.\S+/;
      return res.test(email);
    },

    /**
     * Thực hiện kiểm tra identity có đúng định dạng hay không
     * @param {string} identityNumber identityNumber cuả nhân viên
     * Author: HVDUNG (11/06/2022)
     */
    checkIdentityNumber(identityNumber) {
      if (identityNumber.length != 12 || !this.isNumeric(identityNumber))
        return false;
      return true;
    },
     isNumeric(str) {
        if (typeof str != "string") return false // we only process strings!  
        return !isNaN(str) && // use type coercion to parse the _entirety_ of the string (`parseFloat` alone does not do this)...
         !isNaN(parseFloat(str)) // ...and ensure strings of whitespace fail
    },

    /**
     * Thực hiện lấy mã nhân viên mới
     * Author: HVDUNG (24/06/2022)
     */
    async getNewEmployeeCode() {
      try {
        var me = this;
        // nếu không có id thì gán lại employee là null
        // me.employee = {};
        // gọi API lấy mã nhân viên
        await axios
          .get("http://localhost:22454/api/v1/Employees/NewEmployeeCode")
          .then(function (res) {
            me.employee.EmployeeCode = res.data;
            // thực hiện focus vào ô nhập liệu đầu tiên
            document.getElementById("EmployeeCode").focus();
          })
          .catch(function (res) {
            console.log(res);
          });
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Thực hiện validate dữ liệu
     * true: validate thành công
     * false: validate có lỗi
     * Author: HVDUNG (05/06/2022)
     */
    validate() {
      var me = this;
      var isValid = true;
      //1. validate dữ liệu
      var arrayErrors = [];
      if (!me.employee.EmployeeCode) {
        arrayErrors.push(miSaResource.VI["EmployeeCodeNotNull"]);
      }

      if (!me.employee.EmployeeName) {
        arrayErrors.push(miSaResource.VI.EmployeeNameNotNull);
      }
      if (!me.employee.DepartmentId) {
        arrayErrors.push(miSaResource.VI.DepartmentNameNotNull);
      }
       if (!me.employee.EmployeePosition) {
        arrayErrors.push(miSaResource.VI.EmployeePositionNotNull);
      }
      // convert về dạng ngày tháng thì mới có thể so sánh được
      var dateCurrent = document.getElementById("date_timepicker_end").value;
      dateCurrent = dateCurrent.split("/").reverse().join("-");
      dateCurrent = new Date(dateCurrent);
      if (dateCurrent > new Date()) {
        arrayErrors.push(miSaResource.VI.CheckDateOfBirth);
      }
      // convert về dạng ngày tháng thì mới có thể so sánh được
      var dateCurrentIdentityDate = document.getElementById(
        "date_time_IdentityDate"
      ).value;
      dateCurrentIdentityDate = dateCurrentIdentityDate
        .split("/")
        .reverse()
        .join("-");
      dateCurrentIdentityDate = new Date(dateCurrentIdentityDate);
      if (dateCurrentIdentityDate > new Date()) {
        arrayErrors.push(miSaResource.VI.CheckIdentityDate);
      }

      if (me.employee.Email && !me.checkEmail(me.employee.Email)) {
        arrayErrors.push(miSaResource.VI.CheckEmail);
      }
      if (me.employee.IdentityNumber && !me.checkIdentityNumber(me.employee.IdentityNumber)) {
        arrayErrors.push(miSaResource.VI.InvalidIdentityFormat);
      }

      // thực hiện validate các trường khi là nhà cung cấp
      if(me.employee.IsOrganizations){
         if (!me.employee.OrganizationName) {
        arrayErrors.push(miSaResource.VI["OrganizationNameNotNull"]);
      }
         if (!me.employee.TaxCode) {
        arrayErrors.push(miSaResource.VI["TaxCodeNotNull"]);
      }
         if (!me.employee.OrganizationAddress) {
        arrayErrors.push(miSaResource.VI["OrganizationAddressNotNull"]);
      }
      }

      if (arrayErrors.length>0) {
        isValid = false;
        //hiển thị thông báo validate không hợp lệ
        let noticeDialog = document.getElementsByClassName("d-dialog-box")[0];
        let bodyTextNoticeDialog = noticeDialog.querySelector(".d-dialog-body");
        bodyTextNoticeDialog.innerHTML = "";
        //append nội dung thông báo
        arrayErrors.forEach((errorMsg) => {
          var el = document.createElement("div");
          el.innerHTML = `${errorMsg}`;
          bodyTextNoticeDialog.append(el);
        });
        document.getElementsByClassName("d-dialog-box")[0].style.display =
          "block";
        // return;
      }
      return isValid;
    },

    /**
     * Thực hiện cập nhật lại dữ liệu sau khi thực hiện thay đổi danh sách nhân viên
     * Author: HVDUNG (20/07/2022)
     */
    async resetEmployee() {
      var me = this;
      // thực hiện cập nhật lại dữ liệu lên giao diện
      try {
        await axios
          .get(
            `http://localhost:22454/api/v1/Employees/filter?pageSize=${me.pageSize}&pageNumber=${me.pageNumberSelected}&employeeFilter=${me.textSearch}
          &bankName=${me.bankName}&gender=${me.gender}&departmentId=${me.departmentId}&IsOrganizations=${me.checkOrganization}`
          )
          .then((response) => {
            me.employees = response.data.Data;
            me.$emit("changeValueEmployees", me.employees);
            me.$emit("changeCount", response.data.TotalRecords);
          })
          .catch(function (error) {
            console.log(error);
          });
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Thực hiện thêm mới nhân viên
     * Author: HVDUNG (05/06/2022)
     */
    async addEmployee() {
      var me = this;
      document.getElementsByClassName("loading")[0].style.display = "block";
      // gán một positionId mặc định cho employee
      // me.employee.PositionId = "25c6c36e-1668-7d10-6e09-bf1378b8dc91";
      // gán giá trị mặc định cho gender
      await axios
        .post("http://localhost:22454/api/v1/Employees", me.employee)
        .then(function (res) {
          console.log(res);
          // dùng tạm thời => phải dùng toast msg
        })
        .catch(function (res) {
          let error = res.response.status;
          switch (error) {
            case 400:
            case 500:
              // hiển thị thông báo lỗi khi thêm mới
              alert(res.response.data.Data.EmployeeCode);
              // alert(res.response.data.Data);
              // console.log(res.response);
              break;

            default:
              break;
          }
        });
      // Thực hiện cập nhật lại danh sách nhân viên sau khi thêm mới
      await me.resetEmployee();
      //ẩn loading
      document.getElementsByClassName("loading")[0].style.display = "none";
    },

    /**
     * Thực hiện cập nhật thông tin nhân viên
     * thông tin nhân viên được cập nhật
     * Author: HVDUNG (05/06/2022)
     */
    async updateEmployee() {
      var me = this;
      //hiển thị loading
      document.getElementsByClassName("loading")[0].style.display = "block";
      // thực hiện update nhân viên
      await axios
        .put(
          `http://localhost:22454/api/v1/Employees/${me.employeeIdSelected}`,
          me.employee
        )
        .then(function (res) {
          console.log(res);
        })
        .catch(function (res) {
          alert(res.response.data.Data.EmployeeCode);
        });
      // thực hiện cập nhật lại dữ liệu lên giao diện
      await me.resetEmployee();
      //ẩn loading
      document.getElementsByClassName("loading")[0].style.display = "none";
    },
  },
};
</script>

<style>
@import url(../../style/css/page/employee-info.css);
@import url(../../style/css/icon/icon.css);
</style>