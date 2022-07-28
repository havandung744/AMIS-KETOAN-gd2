<template>
  <div class="d-dialog-box-delete">
    <div class="d-dialog">
      <div class="d-dialog-header"></div>
      <div class="d-dialog-content-box">
        <div class="d-dialog-icon-warning">
          <div class="icon_warning"></div>
        </div>
        <div class="d-dialog-body">content</div>
      </div>
      <div class="d-dialog-footer">
        <div class="d-dialog-footer-line"></div>
        <button class="d-btn" id="d-close" @click="btnCloseOnClick">Không</button>
        <button class="d-btn" id="d-save" @click="btnSaveOnClick">Có</button>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
export default {
  props: ["employeeIdForDelete","arrayEmployeeId", "pageNumberSelected", "pageSize", "textSearch", "bankName", "gender", "departmentId"],
  name: "DialogDelete",
  methods: {
    /**
     * Thực hiện xóa dữ liệu
     * Author: HVDUNG (05/06/2022)
     */
    async btnSaveOnClick() {

      var me = this;
      // Thực hiện chuyển mảng id về dạng chuỗi
      try {
        var employeeIds;
        if(me.employeeIdForDelete==null){
          employeeIds = me.arrayEmployeeId.toString();
        }
        else{
         employeeIds = me.employeeIdForDelete;
        }
        await axios
          .delete(`http://localhost:22454/api/v1/Employees/DeleteMultiEmployee?employeeIdList=${employeeIds}`)
          .then((response) => {
            console.log(response);
            // thực hiện hide checked sau khi xóa nhiều
             document.getElementById('checkAll').checked=false;
             me.$emit("arrayEmployeeId",[]);
          })
          .catch(function (error) {
            console.log(error);
          });
      } catch (error) {
        me.isLoading = false;
        console.log(error);
      }
      // var me = this;
      // await axios
      //   .delete(
      //     `http://localhost:22454/api/v1/Employees/${me.employeeIdForDelete}`
      //   )
      //   .then(function (res) {
      //     console.log(res);
      //   })
      //   .catch(function (res) {
      //     console.log(res);
      //   });

      // thực hiện loadding lại dữ liệu
      try {
        //hiện loading
        document.getElementsByClassName("loading")[0].style.display = "block";
        await axios
          .get(`http://localhost:22454/api/v1/Employees/filter?pageSize=${me.pageSize}&pageNumber=${me.pageNumberSelected}&employeeFilter=${me.textSearch}
          &bankName=${me.bankName}&gender=${me.gender}&departmentId=${me.departmentId}`)
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
      //ẩn form dialogDelete
      document.getElementsByClassName("d-dialog-box-delete")[0].style.display = "none";
      //ẩn loading
      document.getElementsByClassName("loading")[0].style.display = "none";
    },

    /**
     * Thực hiện đóng form xóa
     * Author: HVDUNG (25/06/2022)
     */
    btnCloseOnClick() {
      document.getElementsByClassName("d-dialog-box-delete")[0].style.display = "none";
    },
  },
};
</script>

<style scoped>
@import url("../../style/css/icon/icon.css");

.d-dialog-box-delete {
  display: none;
  position: fixed;
  top: 0;
  left: 0;
  bottom: 0;
  right: 0;
  background-color: rgba(177, 177, 177, 0.397);
  z-index: 100;
}

.d-dialog-body {
  margin-top: 20px;
}

#d-close{
  color: #000000;
  background-color: #fff;
  border: 1px solid #bbbbbb;
}

</style>>