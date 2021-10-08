<template>
  <div>
    <div class="table-responsive">
      <table class="table">
        <thead class="bg-success text-white">
          <tr>
            <th scope="col">#</th>
            <th scope="col">Identification</th>
            <th scope="col">Name</th>
            <th scope="col"></th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(client, index) in clients" :key="index">
            <th scope="row">{{ index + 1 }}</th>
            <td>{{ client.identification }}</td>
            <td>{{ client.name }}</td>
            <td>
              <button
                class="btn btn-secondary"
                type="button"
                @click="editClient(client)"
              >
                Edit
              </button>
              |
              <button
                class="btn btn-danger"
                type="button"
                @click="deleteClient(client.id)"
              >
                Delete
              </button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <div>
      <client-edit-form
        :identification="currentClient ? currentClient.identification : ''"
        :name="currentClient ? currentClient.name : ''"
        :description="currentClient ? currentClient.description : ''"
        :addresses="currentClient ? currentClient.addresses : []"
        @saveClient="updateClient($event)"
      />
    </div>
  </div>
</template>

<script>
import clientForm from "./EditForm.vue";
import clientService from "../../services/ClientService";

export default {
  name: "client-list",
  components: {
    "client-edit-form": clientForm,
  },
  props: ["clients"],
  data() {
    return {
      currentClient: null,
    };
  },
  methods: {
    updateClient(client) {
      this.currentClient.identification = client.identification;
      this.currentClient.description = client.description;
      this.currentClient.name = client.name;
      this.currentClient.addresses = client.addresses;

      clientService
        .update(this.currentClient)
        .then(() => {
          window.$("#EditFormModal").modal("hide");
          this.$swal(
            "Great!",
            "You're client was updated successfully!",
            "success"
          ).then(() => {
            this.$emit("refresh");
          });
        })
        .catch((err) => {
          this.$swal("Opps!", "Something went wrong!", "error");
          window.console.log(err);
        });
    },
    editClient(client) {
      this.currentClient = { ...client };
      window.$("#EditFormModal").modal("show");
    },
    deleteClient(id) {
      this.$swal({
        title: "Are you sure?",
        text: "Once deleted, you won't see this client!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!",
      }).then((data) => {
        if (data.isConfirmed) {
          clientService.delete(id).then(() => {
            this.$swal(
              "Great!",
              "You're client was removed successfully!",
              "success"
            ).then(() => {
              this.$emit("refresh");
            });
          });
        }
      });
    },
  },
};
</script>