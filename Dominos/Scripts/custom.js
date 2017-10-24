var productListApp = new Vue({
  el: '#product-list-app',
  data: {
    products: [],
  },
  created: function () {
      this.getProducts();
  },
  methods: {
      getProducts: function () {
          let self = this;
          axios.get('/RESTService.svc/products/list')
              .then(function (response) {
                  self.products = response.data;
              })
              .catch(function (error) {
                  console.log(error);
              });
      }
  }
})