<template>
  <div class="d-content">
    <div class="d-page-header">
      <div class="d-page-title">Nhân viên</div>
      <button
        class="d-btn d-btn"
        id="d-btn-add-employee"
        @click="btnAddOnclick"
      >
        Thêm mới nhân viên
      </button>
    </div>

    <div class="d-page-toolbar">
      <div class="d-page-toolbar-left">
        <button id="batch-execution" @click="btnOnClickBatchExecution">
          Thực hiện hàng loạt
          <div class="icon-expand-more-box">
            <div class="icon_expand_more"></div>
          </div>
          <button
            class="deleteMany"
            v-show="isShowDeleteMany"
            @click="deleteMany"
          >
            Xóa
          </button>
        </button>

        <el-select
          v-model="this.departmentId"
          placeholder="Chọn phòng ban"
          @change="onChangeDepartmentId()"
          class="m-2"
        >
          <el-option
            v-for="item in this.departments"
            :key="item.value"
            :label="item.label"
            :value="item.value"
          />
        </el-select>

        <el-select
          v-model="this.bankName"
          placeholder="Chọn ngân hàng"
          @change="onChangeBankName()"
          class="m-2"
        >
          <el-option
            v-for="item in this.bankNames"
            :key="item.value"
            :label="item.label"
            :value="item.value"
          />
        </el-select>

        <el-select
          v-model="this.gender"
          placeholder="Chọn giới tính"
          @change="onChangeGender()"
          class="m-2"
        >
          <el-option
            v-for="item in this.genders"
            :key="item.value"
            :label="item.label"
            :value="item.value"
          />
        </el-select>
        <el-select
          v-model="this.checkOrganization"
          placeholder="Chọn vai trò"
          @change="onChangeOrganization()"
          class="m-2"
        >
          <el-option
            v-for="item in this.organizations"
            :key="item.value"
            :label="item.label"
            :value="item.value"
          />
        </el-select>
      </div>

      <div class="d-toolbar-left">
        <input
          type="text"
          class="d-input"
          v-on:keyup="autoSearch"
          v-model="textSearch"
          placeholder="Tìm kiếm theo mã, tên nhân viên"
          style="font-size: 12px"
        />
        <div class="icon_search"></div>
      </div>
      <div class="icon_refresh" @click="btnRefreshOnClick"></div>
      <div class="icon_excel" @click="btnExportExcel"></div>
    </div>

    <div class="d-grid">
      <div class="d-grid-table">
        <table class="d-table">
          <thead>
            <tr>
              <!-- <th class="text-align-left ok">STT</th> -->
              <!-- <th class="text-align-center"><input type="checkbox"></th> -->
              <th class="text-align-center">
                <input
                  type="checkbox"
                  id="checkAll"
                  @click="getAllEmployees($event)"
                />
              </th>
              <th class="text-align-left">MÃ NHÂN VIÊN</th>
              <th class="text-align-left">TÊN NHÂN VIÊN</th>
              <th class="text-align-left">GIỚI TÍNH</th>
              <th class="text-align-center">NGÀY SINH</th>
              <th class="text-align-left">SỐ CMND</th>
              <th class="text-align-left">CHỨC DANH</th>
              <th class="text-align-left">TÊN ĐƠN VỊ</th>
              <th class="text-align-left">SỐ TÀI KHOẢN</th>
              <th class="text-align-left">TÊN NGÂN HÀNG</th>
              <th class="text-align-left">CHI NHÁNH TK NGÂN HÀNG</th>
              <th class="text-align-left">TÊN TỔ CHỨC</th>
              <th class="text-align-left">MÃ SỐ THUẾ</th>
              <th class="text-align-left">ĐỊA CHỈ TỔ CHỨC</th>
              <th class="text-align-center" style="min-width: 119px">
                CHỨC NĂNG
              </th>
            </tr>
          </thead>
          <tbody>
            <tr
              v-for="employee in employees"
              :key="employee.EmployeeId"
              @dblclick="trOnDoubleClick(employee)"
            >
              <td id="d-td" class="text-align-center">
                <input
                  type="checkbox"
                  @change="check($event, employee.EmployeeId)"
                />
              </td>
              <td class="text-align-left">{{ employee.EmployeeCode }}</td>
              <td class="text-align-left">{{ employee.EmployeeName }}</td>
              <td class="text-align-left">{{ employee.GenderName }}</td>
              <td class="text-align-left">
                {{ formatDate(employee.DateOfBirth) }}
              </td>
              <td class="text-align-left">{{ employee.IdentityNumber }}</td>
              <td class="text-align-left">{{ employee.EmployeePosition }}</td>
              <td class="text-align-left">{{ employee.DepartmentName }}</td>
              <td class="text-align-left">{{ employee.BankAccountNumber }}</td>
              <td class="text-align-left">{{ employee.BankName }}</td>
              <td class="text-align-left">{{ employee.BankBranchName }}</td>
              <td class="text-align-left">{{ employee.OrganizationName }}</td>
              <td class="text-align-left">{{ employee.TaxCode }}</td>
              <td class="text-align-left">
                {{ employee.OrganizationAddress }}
              </td>
              <td class="text-align-center">
                <div class="d-function">
                  <div class="d-text">Sửa</div>
                  <div
                    class="d-dropdown_box"
                    @click="dropdownClick(employee, $event)"
                  >
                    <div class="icon_dropdown"></div>
                  </div>
                </div>
              </td>
            </tr>
          </tbody>
        </table>
        <DialogNoEmployee
          v-show="checkTotalEmployee == true"
        ></DialogNoEmployee>
      </div>
      <div class="d-paging" v-show="checkTotalEmployee == false">
        <div class="d-paging-left">
          Tổng số: <b>{{ this.count }}</b> bản ghi
        </div>
        <div class="d-paging-center"></div>
        <div class="d-paging-right">
          <div class="d-row">
            <select class="d-combobox" v-model="pageSize">
              <option value="10">10 bản ghi trên 1 trang</option>
              <option value="20">20 bản ghi trên 1 trang</option>
              <option value="30">30 bản ghi trên 1 trang</option>
              <option value="50">50 bản ghi trên 1 trang</option>
              <option value="100">100 bản ghi trên 1 trang</option>
            </select>
          </div>
          <div class="d-row">
            <PaginateList
              :totalPages="totalPages"
              @pageNumber="pageNumber"
            ></PaginateList>
          </div>
        </div>
      </div>
    </div>
    <!-- showDialog là props của file detail, giá trị nhận là isShowDialog của file EmployeeList -->
    <!-- @isShowDialog là cách ta định nghĩa một phương thức để từ bên detail có thể gọi sang bên list thông qua $emit -->
    <!-- "employee" là cách ta truyền trực tiếp từ cliend chứ không thông qua server-->
    <EmployeeDetail
      @changeValueEmployees="changeValueEmployees"
      @changeValueFormMode="changeValueFormMode"
      :checkReplication="checkReplication"
      v-show="isShowDialog"
      @isShowDialog="toggleEmployeeDialog"
      :employeeIdSelected="employeeIdSelected"
      :formMode="formDetailMode"
      :pageNumberSelected="pageNumberSelected"
      :pageSize="pageSize"
      @changeCount="changeCount"
      :textSearch="textSearch"
      :bankName="bankName"
      :gender="gender"
      :departmentId="departmentId"
      :checkOrganization="checkOrganization"
    >
    </EmployeeDetail>
    <!-- thực hiện hiển thị thông báo khi validate không hợp lệ-->
    <DialogNotice></DialogNotice>
    <!-- thực hiện hiển thị cảnh báo khi thực hiện xóa nhân viên-->
    <DialogDelete
      @changeValueEmployees="changeValueEmployees"
      :employeeIdForDelete="employeeIdForDelete"
      @arrayEmployeeId="changeArrayEmployeeId"
      :arrayEmployeeId="arrayEmployeeId"
      @pageNumber="pageNumber"
      :pageNumberSelected="pageNumberSelected"
      :pageSize="pageSize"
      :textSearch="textSearch"
      :bankName="bankName"
      :gender="gender"
      :departmentId="departmentId"
      :checkOrganization="checkOrganization"
      @changeCount="changeCount"
    >
    </DialogDelete>

    <!-- thực hiện hiển thị dropdown khi click -->
    <DropDown
      v-show="isShowDropDown"
      @isShowDropDown="toggleDropDown"
      @employeeIdSelected="changeEmployeeIdSelected"
      id="d-dropDown"
      :employeeCodeForDelete="employeeCodeForDelete"
      @isShowDialog="toggleEmployeeDialog"
    ></DropDown>
    <!-- thành phần thực hiện loading -->
    <div class="loading" v-show="isLoading">
      <div class="loading-icon"></div>
    </div>
  </div>
</template>

<script>
import DropDown from "../page/DropDownList.vue";
import axios from "axios";
import EmployeeDetail from "./EmployeeDetail.vue";
import DialogNotice from "../dialog/DialogNotice.vue";
import PaginateList from "../common/PaginateList.vue";
import DialogDelete from "../dialog/DialogDelete.vue";
import DialogNoEmployee from "../dialog/DialogNoEmployee.vue";
import { miSaResource } from "../../js/miSaResource";
export default {
  name: "EmployeeList",
  components: {
    DropDown,
    EmployeeDetail,
    DialogNotice,
    PaginateList,
    DialogDelete,
    DialogNoEmployee,
  },

  /**
   * Thực hiện khởi tạo đối tượng employees
   * Author: HVDUNG(05/06/2022)
   */
  created() {
    // giá trị khởi tạo ban đầu dành cho việc hiển thị
    // this.pagination(10, 1, "", "", "", "");
    this.bankName = localStorage.getItem("bankName");
    this.gender = localStorage.getItem("gender");
    this.departmentId = localStorage.getItem("departmentId");
    this.checkOrganization = localStorage.getItem("checkOrganization");
    if (!this.bankName) this.bankName = "";
    if (!this.gender) this.gender = "";
    if (!this.departmentId) this.departmentId = "";
    if (!this.checkOrganization) this.checkOrganization = "false";
    this.pagination(
      10,
      1,
      "",
      this.bankName,
      this.gender,
      this.departmentId,
      this.checkOrganization
    );
  },

  watch: {
    // cập nhật lại tổng số bản ghi khi có sự thay đổi
    count: function (value) {
      this.count = value;
    },

    // thực hiện focus nhiều lần vào cùng 1 tr thì không bị mất focus
    isShowDialog: function () {
      if (this.isShowDialog == false) this.employeeIdSelected = null;
    },

    // kiểm tra sự thay đổi của pageSize và cập nhật lại dữ liệu
    pageSize: function (value) {
      this.pagination(
        value,
        1,
        this.textSearch,
        this.bankName,
        this.gender,
        this.departmentId,
        this.checkOrganization
      );
    },

    // kiểm tra sự thay đổi của pageNumberSelected và cập nhật lại dữ liệu
    pageNumberSelected: function (value) {
      this.pagination(
        this.pageSize,
        value,
        this.textSearch,
        this.bankName,
        this.gender,
        this.departmentId,
        this.checkOrganization
      );
    },
  },

  methods: {
    onChangeDepartmentId() {
      localStorage.setItem("departmentId", this.departmentId);
      this.pagination(
        this.pageSize,
        1,
        this.textSearch,
        this.bankName,
        this.gender,
        this.departmentId,
        this.checkOrganization
      );
    },

    onChangeBankName() {
      localStorage.setItem("bankName", this.bankName);
      this.pagination(
        this.pageSize,
        1,
        this.textSearch,
        this.bankName,
        this.gender,
        this.departmentId,
        this.checkOrganization
      );
    },
    onChangeGender() {
      localStorage.setItem("gender", this.gender);
      this.pagination(
        this.pageSize,
        1,
        this.textSearch,
        this.bankName,
        this.gender,
        this.departmentId,
        this.checkOrganization
      );
    },
    onChangeOrganization() {
      localStorage.setItem("checkOrganization", this.checkOrganization);
      this.pagination(
        this.pageSize,
        1,
        this.textSearch,
        this.bankName,
        this.gender,
        this.departmentId,
        this.checkOrganization
      );
    },

    /**
     * Thực hiện thay đổi danh sách id của nhân viên
     * @param {mảng id của nhân viên} value
     * Author: HVDUNG(19/07/2022)
     */
    changeArrayEmployeeId(value) {
      this.arrayEmployeeId = value;
    },

    /**
     * Thực hiện xóa nhiều
     * checked all mọi checkbox khi click vào input checkbox
     * lấy ra danh sách id của mọi employee được checkbox
     * @param {} event
     * Author: HVDUNG(19/07/2022)
     */
    getAllEmployees(event) {
      var checkboxes = document.querySelectorAll("input[type='checkbox']");
      if (event.target.checked) {
        checkboxes.forEach(function (checkbox) {
          checkbox.checked = true;
        });
        this.pagination(
          this.pageSize,
          this.pageNumberSelected,
          this.textSearch,
          this.bankName,
          this.gender,
          this.departmentId,
          this.checkOrganization
        );
        for (var emp of this.employees) {
          this.arrayEmployeeId.push(emp.EmployeeId);
        }
      } else {
        checkboxes.forEach(function (checkbox) {
          checkbox.checked = false;
        });
        this.arrayEmployeeId = [];
      }
    },

    /**
     * Thực hiện kiểm tra các nhân viên nào đã được checked
     * @param {*} e event có sẵn
     * @param {*} value id của nhân viên
     * Author: HVDUNG(17/07/2022)
     */
    check(e, value) {
      if (e.target.checked) this.arrayEmployeeId.push(value);
      else {
        var index = this.arrayEmployeeId.indexOf(value);
        if (index !== -1) this.arrayEmployeeId.splice(index, 1);
      }
    },

    /**
     * Thực hiện hiển thị/ẩn button xóa khi click vào button thực hiện hàng loạt
     * Author: HVDUNG(17/07/2022)
     */
    btnOnClickBatchExecution() {
      if (this.arrayEmployeeId.length > 1)
        this.isShowDeleteMany = !this.isShowDeleteMany;
      else this.isShowDeleteMany = false;
    },

    /**
     * Thực hiện xóa nhiều khi click button xóa
     * Author: HVDUNG(17/07/2022)
     */
    async deleteMany() {
      this.employeeIdForDelete = null;
      let noticeDialog = document.getElementsByClassName(
        "d-dialog-box-delete"
      )[0];
      let bodyTextNoticeDialog = noticeDialog.querySelector(
        ".d-dialog-box-delete .d-dialog-body"
      );
      bodyTextNoticeDialog.innerHTML = miSaResource.VI.deleteMany;
      document.getElementsByClassName("d-dialog-box-delete")[0].style.display =
        "block";
    },

    /**
     * Thực hiện thay đổi giá trị employeeIdSelected
     * @param {*} value
     * Author: HVDUNG(15/07/2022)
     */
    changeEmployeeIdSelected(value) {
      this.employeeIdSelected = value;
    },

    /**
     * thực hiện hiển thị form dropdown khi click và lấy được mã nhân viên được click
     * Author: HVDUNG(10/06/2022)
     */
    dropdownClick(employee, event) {
      this.toggleDropDown();
      var x = event.pageX - 105;
      var y = event.pageY + 12;
      document.getElementById("d-dropDown").style.left = `${x}px`;
      document.getElementById("d-dropDown").style.top = `${y}px`;
      this.employeeIdForDelete = employee.EmployeeId;
      this.employeeCodeForDelete = employee.EmployeeCode;
      this.employeeIdSelected = this.employeeIdForDelete;
      this.formDetailMode = 1;
      this.checkReplication = true;
    },

    /**
     * Thực hiện kiểm tra page đang chọn
     * @param {int} value giá trị
     * Author: HVDUNG(10/06/2022)
     */
    pageNumber(value) {
      this.pageNumberSelected = value;
    },

    /**
     * Thực hiện phân trang
     * @param {int} pageSize số bản ghi/trang
     * @param {int} pageNumber trang thứ bao nhiêu
     * @param {string} textSearch nội dung tìm kiếm
     * Author: HVDUNG(08/06/2022)
     */
    // async pagination(pageSize, pageNumber, textSearch) {
    //   var me = this;
    //   try {
    //     me.isLoading = true;
    //     await axios
    //       .get(
    //         `http://localhost:22454/api/v1/Employees/filter?pageSize=${pageSize}&pageNumber=${pageNumber}&employeeFilter=${textSearch}`
    //       )
    //       .then((response) => {
    //         console.log(response.data);
    //         me.employees = response.data.Data;
    //         me.totalPages = response.data.TotalPages;
    //         me.count = response.data.TotalRecords;
    //         // thực hiện hiển thị icon và text không có dữ liệu khi tìm kiếm
    //         if (me.totalPages == 0) me.checkTotalEmployee = true;
    //         else me.checkTotalEmployee = false;
    //         console.log(me.textSearch);
    //       })
    //       .catch(function (error) {
    //         console.log(error);
    //       });
    //     me.isLoading = false;
    //   } catch (error) {
    //     me.isLoading = false;
    //     console.log(error);
    //   }
    // },

    async pagination(
      pageSize,
      pageNumber,
      textSearch,
      bankName,
      gender,
      departmentId,
      checkOrganization
    ) {
      var me = this;
      try {
        me.isLoading = true;
        await axios
          .get(
            `http://localhost:22454/api/v1/Employees/filter?pageSize=${pageSize}
            &pageNumber=${pageNumber}&employeeFilter=${textSearch}&bankName=${bankName}&gender=${gender}&departmentId=${departmentId}&IsOrganizations=${checkOrganization}`
          )
          .then((response) => {
            console.log(response.data);
            me.employees = response.data.Data;
            console.log(me.employees);
            me.totalPages = response.data.TotalPages;
            me.count = response.data.TotalRecords;
            // thực hiện hiển thị icon và text không có dữ liệu khi tìm kiếm
            if (me.totalPages == 0) me.checkTotalEmployee = true;
            else me.checkTotalEmployee = false;
            console.log(me.textSearch);
          })
          .catch(function (error) {
            console.log(error);
          });
        me.isLoading = false;
      } catch (error) {
        me.isLoading = false;
        console.log(error);
      }
    },

    /**
     * Thực hiện tự động tìm kiếm khi search
     * Author: VHDUNG(08/06/2022)
     */
    autoSearch() {
      var me = this;
      if (me.textSearch != "") {
        if (me.globalTimeout != null) {
          clearTimeout(me.globalTimeout);
        }
        me.globalTimeout = setTimeout(function () {
          me.globalTimeout = null;
          me.pagination(
            me.pageSize,
            1,
            me.textSearch,
            me.bankName,
            me.gender,
            me.departmentId,
            me.checkOrganization
          );
          console.log(me.textSearch.length);
        }, 500);
      } else
        me.pagination(
          me.pageSize,
          me.pageNumberSelected,
          "",
          me.bankName,
          me.gender,
          me.departmentId,
          me.checkOrganization
        );
    },

    /**
     * thực hiện build lại employees cho cha sau khi nhận sự thay đổi ở các componants khác (vd: EmployeeDeatails, DropDownList)
     * Author: HVDUNG(06/06/2022)
     */
    changeValueEmployees(value) {
      this.employees = value;
    },

    /**
     * thực hiện gán lại value cho formMode
     * Author: HVDUNG(06/06/2022)
     */
    changeValueFormMode(value) {
      this.formDetailMode = value;
    },

    /**
     * thực hiện build lại tổng số bản ghi khi xóa đối tượng
     * Author: HVDUNG(06/06/2022)
     */
    changeCount(value) {
      this.count = value;
    },

    /**
     * Thực hiện show form detail khi click
     * Author: HVDUNG(05/06/2022)
     */
    async btnAddOnclick() {
      // xác nhân không phải là nhân bản
      this.checkReplication = false;
      // gán lại giá trị cho fromDetailMode là thêm mới
      this.formDetailMode = 1;
      // thực hiện xóa đi đường viền đỏ
      document.getElementById("EmployeeCode").classList.remove("d-input-error");
      document.getElementById("EmployeeName").classList.remove("d-input-error");
      document.getElementById("IdentityNumber").classList.remove("d-input-error");
      document.getElementById("EmployeePosition").classList.remove("d-input-error");

      // thực hiện ẩn form dropdown nếu đang mở
      document.getElementsByClassName("dropdown")[0].style.display = "none";
      // thực hiện hiển thị form chi tiết
      this.toggleEmployeeDialog();
      // gán lại giá trị cho employeeIdSelected
      this.employeeIdSelected = "";
    },

    /**
     * Thực hiện hiển thị thông tin nhân viên
     * @param {*} employee là đối tượng emplye Được lấy ở v-for
     * building ra data của employee
     * Author: HVDUNG(05/06/2022)
     */
    trOnDoubleClick(employee) {
      // gán lại chế độ xem chi tiết hay là nhân bản
      this.checkReplication = false;
      // gán chế độ là update
      this.formDetailMode = 0;
      // thực hiện xóa đi đường viền đỏ
      document.getElementById("EmployeeCode").classList.remove("d-input-error");
      document.getElementById("EmployeeName").classList.remove("d-input-error");
      document.getElementById("IdentityNumber").classList.remove("d-input-error");
      document.getElementById("EmployeePosition").classList.remove("d-input-error");

      // thực hiện ẩn form dropdown nếu đang mở
      document.getElementsByClassName("dropdown")[0].style.display = "none";
      // hiển thị form chi tiết
      this.toggleEmployeeDialog();
      this.employeeIdSelected = employee.EmployeeId;
    },

    // thực hiện việc ẩn hiện form chi tiết
    // Author: HVDUNG(08/06/2022)
    toggleEmployeeDialog() {
      this.isShowDialog = !this.isShowDialog;
    },

    // thực hiện việc ẩn hiện form dropDown
    // Author: HVDUNG(08/06/2022)
    toggleDropDown() {
      this.isShowDropDown = !this.isShowDropDown;
    },

    /**
     * Thực hiện reload lại dữ liệu khi click
     * Author: HVDUNG(05/06/2022)
     */
    btnRefreshOnClick() {
      if (this.textSearch == "") {
        this.pagination(
          this.pageSize,
          this.pageNumberSelected,
          "",
          this.bankName,
          this.gender,
          this.departmentId,
          this.checkOrganization
        );
      } else {
        this.pagination(
          this.pageSize,
          1,
          this.textSearch,
          this.bankName,
          this.gender,
          this.departmentId,
          this.checkOrganization
        );
      }
    },

    /**
     * Thực hiện xuất file excel
     * Author: HVDUNG(25/06/2022)
     */
    btnExportExcel() {
      try {
        axios
          .get(
            `http://localhost:22454/api/v1/Employees/excel?employeeFilter=${this.textSearch}
          &bankName=${this.bankName}&gender=${this.gender}&departmentId=${this.departmentId}&IsOrganizations=${this.checkOrganization}`,
            {
              responseType: "blob",
            }
          )
          .then((res) => {
            const url = URL.createObjectURL(new Blob([res.data]));
            const link = document.createElement("a");

            link.href = url;
            link.setAttribute("download", "Danh sách nhân viên.xlsx");

            document.body.appendChild(link);
            link.click();
          })
          .catch((res) => {
            console.log(res);
          });
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Thực hiện định dạng lại ngày tháng để hiển thị
     * @param {*} dateOfBirth ngày tháng được truyền vào
     * Author: HVDUNG(05/06/2022)
     */
    formatDate(dateOfBirth) {
      if (dateOfBirth) {
        // chuyển từ dạng string sang dạng Date
        dateOfBirth = new Date(dateOfBirth);
        // lấy ngày
        let date = dateOfBirth.getDate();
        // lấy tháng
        let month = dateOfBirth.getMonth() + 1;
        // lấy năm
        let year = dateOfBirth.getFullYear();
        // thêm số 0 vào trước nếu chỉ có một kí tự
        date = date < 10 ? `0${date}` : date;
        month = month < 10 ? `0${month}` : month;
        return `${date}/${month}/${year}`;
      } else {
        return "";
      }
    },
  },

  data() {
    return {
      textSearch: "",
      bankName: "",
      gender: "",
      checkOrganization: "",
      departmentId: "",
      employees: {},
      isShowDialog: false,
      isLoading: true,
      employeeIdSelected: null,
      employeeIdForDelete: null,
      employeeCodeForDelete: null,
      active: false,
      formDetailMode: 0,
      isShowDropDown: false,
      count: 0,
      globalTimeout: null,
      // data dùng để phân trang
      totalPages: 0,
      pageSize: 10,
      pageNumberSelected: 1,
      checkTotalEmployee: false,
      checkReplication: false,
      arrayEmployeeId: [],
      isShowDeleteMany: false,
      departments: [
        {
          value: "",
          label: "Chọn phòng ban",
        },
        {
          value: "142cb08f-7c31-21fa-8e90-67245e8b283e",
          label: "Phòng đào tạo",
        },
        {
          value: "17120d02-6ab5-3e43-18cb-66948daf6128",
          label: "Phòng nhân sự",
        },
        {
          value: "469b3ece-744a-45d5-957d-e8c757976496",
          label: "Phòng sản xuất",
        },
        {
          value: "4e272fc4-7875-78d6-7d32-6a1673ffca7c",
          label: "Phòng kế toán",
        },
      ],
      bankNames: [
        {
          value: "",
          label: "Chọn ngân hàng",
        },
        {
          value: "ACB",
          label: "ACB",
        },
        {
          value: "AgriBank",
          label: "AgriBank",
        },
        {
          value: "BIDV",
          label: "BIDV",
        },
        {
          value: "VPBank",
          label: "VPBank",
        },
      ],
      genders: [
        {
          value: "",
          label: "Chọn giới tính",
        },
        {
          value: "1",
          label: "Nam",
        },
        {
          value: "0",
          label: "Nữ",
        },
        {
          value: "2",
          label: "Khác",
        },
      ],
      organizations: [
        {
          value: "false",
          label: "Khách hàng",
        },
        {
          value: "true",
          label: "Nhà cung cấp",
        },
      ],
    };
  },
};
</script>

<style scoped>
@import url(../../style/css/layout/content.css);
@import url(../../style/css/components/combobox.css);
</style>