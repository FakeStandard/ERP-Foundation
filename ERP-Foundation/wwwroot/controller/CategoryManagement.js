Vue.config.devtools = true;
Vue.config.debug = true;

var vm = new Vue({
    el: '#app',
    data: {
        loading: true,
        // 載入資料
        list: [],
        // 當前頁數
        page: 1,
        // 每頁顯示的筆數
        pageSize: 10,
        // 總頁數
        pageTotal: 0,
        // 總筆數
        total: 0,
        // 搜尋文字
        searchText: '',
        addForm: {
            name: '',
            order: ''
        },
        editForm: {
            id: '',
            name: '',
            order: ''
        },
        // 欄位顯示開關
        NAME: true,
        ORDER: true,
        // 欄位排序
        sortNAME: null,
        sortORDER: null
    },
    methods: {
        getData: function () {
            this.loading = true;
            $.ajax({
                url: GetCategoryURL,
                data: {
                    Page: this.page,
                    PageSize: this.pageSize,
                    SearchText: this.searchText
                },
                type: 'GET',
                success: function (res) {
                    this.pageTotal = res.PageTotal
                    this.Total = res.Total
                    vm.$data.list = res.data
                },
                error: function (error) {
                    console.log(error);
                },
                complete: function () {
                    vm.$data.loading = false;
                }
            });
        },
        searchClick: function () {
            vm.$data.list = []
            this.getData();
        },
        showAddDialogClick: function () {
            this.$data.addForm = {
                name: '',
                order: ''
            }

            $('#AddModal').modal('show');
        },
        showEditDialogClick: function (obj) {
            $('#EditModal').modal('show');

            this.$data.editForm = {
                id: obj.id,
                name: obj.name,
                order: obj.order
            }
        },
        addData: function () {
            $('#AddModal').modal('hide');

            $.ajax({
                url: AddCategoryURL,
                data: this.$data.addForm,
                type: 'POST',
                success: function (res) {
                    swal({
                        title: '新增成功！',
                        type: 'success',
                        showConfirmButton: false,
                        timer: 1000
                    }).catch(swal.noop)
                    vm.getData();
                },
                error: function (err) {
                    console.log(err);
                },
                complete: function () {
                    productVM.getCategoryData();
                }
            });
        },
        editData: function () {
            $('#EditModal').modal('hide');

            $.ajax({
                url: EditCategoryURL,
                data: this.$data.editForm,
                type: 'POST',
                success: function (res) {
                    swal({
                        title: '儲存成功！',
                        type: 'success',
                        showConfirmButton: false,
                        timer: 1000
                    }).catch(swal.noop)
                    vm.getData();
                },
                error: function (err) {
                    console.log(err);
                },
                complete: function () {
                    productVM.getCategoryData();
                }
            });
        },
        deleteDataClick: function (id) {
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
                        url: DeleteCategoryURL + '/' + id,
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
                            vm.getData();
                            productVM.getCategoryData();
                        }
                    });
                }
            })
        },
        filterSort: function (column) {
            if (column === 'NAME') {
                // do something...
            }
        }
    },
    mounted: function () {
        this.getData();
    }
});