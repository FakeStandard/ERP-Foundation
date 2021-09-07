Vue.config.devtools = true;
Vue.config.debug = true;

var purchaseVM = new Vue({
    el: '#purchaseApp',
    data: {
        loading: true,
        list: {
            orderNo: '',
            factory: '',
            payment: '',
            paymentStatus: '',
            purchaser: '',
            createTime: '',
            items: []
        },
        addForm: {
            index: '',
            itemNo: '',
            name: '',
            quantity: '',
            price: '',
            subTotal: '',
            remark: ''
        },
        editForm: {
            index: '',
            itemNo: '',
            name: '',
            quantity: '',
            price: '',
            subTotal: '',
            remark: ''
        },
        index: 0
    },
    methods: {
        showAddDialogClick() {
            this.$data.addForm = {
                Index: this.index + 1,
                ItemNo: '',
                Name: '',
                Quantity: '',
                Price: '',
                SubTotal: '',
                Remark: ''
            }

            $('#AddPurchaseModal').modal('show');
        },
        addData() {
            this.$data.list.items.push({
                index: this.addForm.index,
                itemNo: this.addForm.itemNo,
                name: this.addForm.name,
                quantity: this.addForm.quantity,
                price: this.addForm.price,
                subTotal: this.addForm.subTotal,
                remark: this.addForm.remark
            })

            $('AddPurchaseModal').modal('hide');
        },
        addAgainData() {
            this.$data.addForm = {
                Index: this.index + 1,
                ItemNo: '',
                Name: '',
                Quantity: '',
                Price: '',
                SubTotal: '',
                Remark: ''
            }
        },
        editData() {
            $('#EditPurchaseModal').modal('show');
        }
        //getData: function () {
        //    this.loading = true;

        //    $.ajax({
        //        url: GetProductURL,
        //        data: {
        //            Page: this.page,
        //            PageSize: this.pageSize,
        //            SearchText: this.searchText
        //        },
        //        type: 'GET',
        //        success: function (res) {
        //            productVM.$data.list = res.data;
        //        },
        //        error: function (err) {
        //            console.log(err);
        //        },
        //        complete: function () {
        //            productVM.$data.loading = false;
        //        }
        //    });
        //}
    },
    mounted: function () {
        //this.getData();
    }
});