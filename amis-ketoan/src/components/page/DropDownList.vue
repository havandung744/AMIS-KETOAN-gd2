<template>
  <div class="dropdown">
    <div>
      <button @click="btnOnReplication">Nhân bản</button>
    </div>
    <div>
      <button @click="btnOnDelete">Xóa</button>
    </div>
    <div>
      <button @click="btnOnStopUsing">Ngừng sử dụng</button>
    </div>
  </div>
</template>

<script>
import { miSaResource } from '@/js/miSaResource';

export default {
  name: "DropDownList",
  props: ["employeeCodeForDelete"],
  methods: {
    /**
     * Thực hiện hiển thị form xác nhận xóa
     * Author: HVDUNG (25/06/2022)
     */
    async btnOnDelete() {
      let noticeDialog = document.getElementsByClassName("d-dialog-box-delete")[0];
      let bodyTextNoticeDialog = noticeDialog.querySelector(".d-dialog-box-delete .d-dialog-body");
      bodyTextNoticeDialog.innerHTML = `Bạn có muốn xóa nhân viên &#60;${this.employeeCodeForDelete}&#62; không?`;
      document.getElementsByClassName("d-dialog-box-delete")[0].style.display =
        "block";
      // ẩn form dropdown
      this.$emit("isShowDropDown");
    },

    /**
     * Thực hiện hiển thị form thông báo
     * Author: HVDUNG (05/06/2022)
     */
    btnOnReplication() {
      this.$emit("isShowDialog");
      // ẩn form dropdown
      this.$emit("isShowDropDown");
       this.$nextTick(() => {
         document.getElementById('EmployeeCode').focus();
      });
    },

    /**
   * Thực hiện hiển thị form thông báo
   * Author: HVDUNG (05/06/2022)
   */
    btnOnStopUsing() {
      let noticeDialog = document.getElementsByClassName("d-dialog-box")[0];
      let bodyTextNoticeDialog = noticeDialog.querySelector(".d-dialog-body");
      bodyTextNoticeDialog.innerHTML = miSaResource.VI.function;
      // hiển thị form notice
      document.getElementsByClassName("d-dialog-box")[0].style.display =
        "block";
      //ẩn form dialogDelete
      document.getElementsByClassName("dropdown")[0].style.display = "none";
    }
  },
};
</script>

<style>
@import url("../../style/css/components/dropdown.css");
</style>