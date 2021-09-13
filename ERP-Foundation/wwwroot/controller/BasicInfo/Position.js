Vue.config.devtools = true;
Vue.config.debug = true;

var positionVM = new Vue({
    el: '#positionApp',
    data: {
        loading: true,
        list: [],
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
            name: ''
        },
        editForm: {
            id: '',
            name: ''
        },
        // 欄位顯示開關
        NAME: true,

        // 欄位排序
        sortNAME: null
    },
    methods: {
        getData: function () {
            this.loading = true;

            $.ajax({
                url: GetPositionURL,
                data: {
                    Page: this.page,
                    PageSize: this.pageSize,
                    SearchText: this.searchText
                },
                type: 'GET',
                success: function (res) {
                    positionVM.$data.list = res.data;
                },
                error: function (err) {
                    console.log(err);
                },
                complete: function () {
                    positionVM.$data.loading = false;
                }
            });
        },
        searchClick() {
            this.getData();
        },
        showAddDialogClick() {
            this.$data.addForm = {
                name: '',
                englishName: '',
                address: '',
                tel: '',
                fax: '',
                contactPerson: '',
                contactTel: '',
                paymentRule: ''
            }

            $('#AddPositionModal').modal('show');
        },
        showEditDialogClick(item) {
            this.$data.editForm = {
                id: item.id,
                name: item.name,
                englishName: item.englishName,
                address: item.address,
                tel: item.tel,
                fax: item.fax,
                contactPerson: item.contactPerson,
                contactTel: item.contactTel,
                paymentRule: item.paymentRule
            }

            $('#EditPositionModal').modal('show');
        },
        addData() {
            $('#AddPositionModal').modal('hide');

            $.ajax({
                url: AddPositionURL,
                data: this.$data.addForm,
                type: 'POST',
                success: function (res) {
                    swal({
                        title: '新增成功！',
                        type: 'success',
                        showConfirmButton: false,
                        timer: 1000
                    }).catch(swal.noop)

                    positionVM.getData();
                },
                error: function (err) {
                    console.log(err);
                }
            });
        },
        editData() {
            $('#EditPositionModal').modal('hide');

            $.ajax({
                url: EditPositionURL,
                data: this.$data.editForm,
                type: 'POST',
                success: function () {
                    swal({
                        title: "儲存成功！",
                        type: 'success',
                        showConfirmButton: false,
                        timer: 1000
                    }).catch(swal.noop)

                    positionVM.getData();
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
                        url: DeletePositionURL + '/' + id,
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
                            positionVM.getData();
                        }
                    });

                }
            })
        }
    },
    mounted: function () {
        this.getData();
    }
});