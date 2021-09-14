Vue.config.devtools = true;
Vue.config.debug = true;

var departmentVM = new Vue({
    el: '#departmentApp',
    data: {
        loading: true,
        list: [],
        parentItem: [],
        searchText: '',
        // 當前頁數
        page: 1,
        // 每頁顯示的筆數
        pageSize: 10,
        // 總頁數
        pageTotal: 0,
        // 總筆數
        total: 0,
        addForm: {
            name: '',
            parentID: '',
        },
        editForm: {
            id: '',
            name: '',
            parentID: '',
        },
        // 欄位顯示開關
        NAME: true,
        PARENTNAME: true,
        // 欄位排序
        //sortNAME: null,
        //sortCONTACTPERSON: null,
        //sortADDRESS: null
    },
    methods: {
        getData: function () {
            this.loading = true;

            $.ajax({
                url: GetDepartmentURL,
                data: {
                    Page: this.page,
                    PageSize: this.pageSize,
                    SearchText: this.searchText
                },
                type: 'GET',
                success: function (res) {
                    departmentVM.$data.list = res.data;
                },
                error: function (err) {
                    console.log(err);
                },
                complete: function () {
                    departmentVM.$data.loading = false;
                }
            });
        },
        getDepartmentItem: function () {
            $.ajax({
                url: GetDepartmentItemURL,
                type: 'GET',
                success: function (res) {
                    departmentVM.$data.parentItem = res
                },
                error: function (res) {
                    console.log(err);
                }
            });
        },
        searchClick() {
            this.getData();
        },
        showAddDialogClick() {
            this.$data.addForm = {
                name: '',
                parentID: ''
            }

            departmentVM.$data.addForm.parentID = this.parentItem[0].key;
            $('#AddDepartmentModal').modal('show');
        },
        showEditDialogClick(item) {
            var key;
            for (var index = 0; index < this.parentItem.length; index++) {
                if (this.parentItem[index].value === item.parentName) {
                    key = this.parentItem[index].key;
                    break;
                }
            }

            this.$data.editForm = {
                id: item.id,
                name: item.name,
                parentID: key
            }

            $('#EditDepartmentModal').modal('show');
        },
        addData() {
            $('#AddDepartmentModal').modal('hide');

            $.ajax({
                url: AddDepartmentURL,
                data: this.$data.addForm,
                type: 'POST',
                success: function (res) {
                    swal({
                        title: '新增成功！',
                        type: 'success',
                        showConfirmButton: false,
                        timer: 1000
                    }).catch(swal.noop)

                    departmentVM.getData();
                    departmentVM.getDepartmentItem();
                    userVM.getDepartmentItem();
                },
                error: function (err) {
                    console.log(err);
                }
            });
        },
        editData() {
            $('#EditDepartmentModal').modal('hide');

            $.ajax({
                url: EditDepartmentURL,
                data: this.$data.editForm,
                type: 'POST',
                success: function () {
                    swal({
                        title: "儲存成功！",
                        type: 'success',
                        showConfirmButton: false,
                        timer: 1000
                    }).catch(swal.noop)

                    departmentVM.getData();
                    departmentVM.getDepartmentItem();
                    userVM.getDepartmentItem();
                },
                error: function (err) {
                    console.log(err);
                }
            });
        },
        deleteDataClick(id) {
            swal({
                title: '確定要刪除？',
                text: '刪除後將無法恢復！',
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: '忍心刪除！',
                cancelButtonText: '取消'
            }).then((confirm) => {
                if (confirm) {
                    $.ajax({
                        url: DeleteDepartmentURL + '/' + id,
                        type: 'POST',
                        success: function () {
                            swal({
                                title: '刪除成功！',
                                text: '您已經刪除該筆資料',
                                type: 'success',
                                timer: 1000,
                                showConfirmButton: false
                            })
                        },
                        error: function () {
                            swal({
                                title: '刪除失敗！',
                                text: '刪除資料失敗,請重新操作',
                                type: 'error',
                                timer: 1000,
                                showConfirmButton: false
                            })
                        },
                        complete: function () {
                            departmentVM.getData();
                        }
                    });

                }
            })
        }
    },
    mounted: function () {
        this.getData();
        this.getDepartmentItem();
    }
});