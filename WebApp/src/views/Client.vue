<template>
  <div>
    <div class="row">
      <div class="col-6">
        <h1>Clients</h1>
      </div>
      <div class="col-6">
        <div class="float-right mt-3">
          <button
            class="btn btn-success"
            data-toggle="modal"
            data-target="#EditFormModal"
            type="button">
            Add Client
          </button>
        </div>
      </div>
    </div>
    <hr />
    <div class="row">
      <div class="col-12">
        <client-list v-if="clients.length > 0" :clients="clients"  @refresh="getClients()" />
      </div>
    </div>

    <client-form @saveClient="createClient($event)" />
  </div>
</template>

<script>
import clientService from "../services/ClientService";
import clientForm from "../components/Clients/CreateForm.vue";
import clientList from "../components/Clients/ClientList.vue";
import Address from "../components/shared/Address.vue";

export default {
  name: "clients",
  components: {
    clientForm,
    clientList,
    Address,
  },
  data: function () {
    return {
      clients: [],
      formTitle: ""      
    };
  },
  mounted: async function () {
    await this.getClients();
  },
  methods: {
    async getClients() {
      const result = await clientService.getAll();
      this.clients = result.data;
    },
    async createClient(client){        
        clientService.create(client).then(() => {
          window.$('#FormModal').modal('hide');          
          this.$swal('Great!', "You're client was saved successfully!", 'success').then(() => {
            this.getClients();
          });          
        }).catch(err => {
          this.$swal('Opps!', "Something went wrong!", 'error');          
          window.console.log(err)
        });
        
    }
  },
};
</script>