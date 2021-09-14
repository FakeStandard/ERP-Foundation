Vue.config.devtools = true;
Vue.config.debug = true;

var titleVM = new Vue({
    el: '#titleApp',
    data: {
        loading: true,
        list: [],
        departmentItem: [],
        unitItem: [],
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
            departmentID: '',
            unitID: ''
        },
        editForm: {
            id: '',
            name: '',
            departmentID: '',
            unitID: ''
        },
        // 欄位顯示開關
        NAME: true,
        DEPARTMENTNAME: true,
        UNITNAME: true,
        // 欄位排序
        //sortNAME: null,
        //sortCONTACTPERSON: null,
        //sortADDRESS: null
    },
    methods: {
        getData: function () {
            this.loading = true;

            $.ajax({
                url: GetTitleURL,
                data: {
                    Page: this.page,
                    PageSize: this.pageSize,
                    SearchText: this.searchText
                },
                type: 'GET',
                success: function (res) {
                    titleVM.$data.list = res.data;
                },
                error: function (err) {
                    console.log(err);
                },
                complete: function () {
                    titleVM.$data.loading = false;
                }
            });
        },
        getDepartmentParentItem: function () {
            $.ajax({
                url: GetDepartmentParentItemURL,
                type: 'GET',
                success: function (res) {
                    titleVM.$data.departmentItem = res;
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
                departmentID: '',
                unitID: ''
            }

            titleVM.$data.addForm.departmentID = this.departmentItem[0].key;
            titleVM.$data.addForm.unitID = this.departmentItem[0].units[0].key;
            titleVM.$data.unitItem = this.departmentItem[0].units;

            $('#AddTitleModal').modal('show');
        },
        showEditDialogClick(item) {
            var departmenetID;
            var unitID;
            for (var i = 0; i < this.departmentItem.length; i++) {
                if (this.departmentItem[i].value === item.departmentName) {
                    departmenetID = this.departmentItem[i].key;
                    titleVM.$data.unitItem = this.departmentItem[i].units;

                    for (var j = 0; j < this.departmentItem[i].units.length; j++) {
                        if (this.departmentItem[i].units[j].value === item.unitName) {
                            unitID = this.departmentItem[i].units[j].key;
                            break;
                        }
                    }
                }
            }

            titleVM.$data.editForm = {
                id: item.id,
                name: item.name,
                departmentID: departmenetID,
                unitID: unitID
            }

            $('#EditTitleModal').modal('show');
        },
        addData() {
            $('#AddTitleModal').modal('hide');

            $.ajax({
                url: AddTitleURL,
                data: this.$data.addForm,
                type: 'POST',
                success: function (res) {
                    swal({
                        title: '新增成功！',
                        type: 'success',
                        showConfirmButton: false,
                        timer: 1000
                    }).catch(swal.noop)

                    titleVM.getData();
                },
                error: function (err) {
                    console.log(err);
                }
            });
        },
        editData() {
            $('#EditTitleModal').modal('hide');

            $.ajax({
                url: EditTitleURL,
                data: this.$data.editForm,
                type: 'POST',
                success: function () {
                    swal({
                        title: "儲存成功！",
                        type: 'success',
                        showConfirmButton: false,
                        timer: 1000
                    }).catch(swal.noop)

                    titleVM.getData();
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
                        url: DeleteTitleURL + '/' + id,
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
                            titleVM.getData();
                        }
                    });

                }
            })
        },
        departmentAddChange: function (event) {
            for (var i = 0; i < this.departmentItem.length; i++) {
                if (this.departmentItem[i].key === parseInt(event.target.value)) {
                    this.unitItem = this.departmentItem[i].units;
                    titleVM.$data.addForm.unitID = this.departmentItem[0].units[0].key;
                    break;
                }
            }
        },
        departmentEditChange: function (event) {
            for (var i = 0; i < this.departmentItem.length; i++) {
                if (this.departmentItem[i].key === parseInt(event.target.value)) {
                    this.unitItem = this.departmentItem[i].units;
                    titleVM.$data.editForm.unitID = this.departmentItem[0].units[0].key;
                    break;
                }
            }
        }
    },
    mounted: function () {
        this.getData();
        this.getDepartmentParentItem();
    }
});