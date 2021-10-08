<template>
  <div class="modal fade" id="FormModal" tabindex="-1" data-backdrop="static">
    <div class="modal-dialog modal-lg">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title">Create Client</h5>
          <button
            type="button"
            class="close"
            data-dismiss="modal"
            aria-label="Close"
          >
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body">
          <form action="">
            <div class="row">
              <div class="col-6">
                <div class="form-group">
                  <label for="identification">Identification</label>
                  <input
                    type="text"
                    name="identification"
                    v-model="identification"
                    class="form-control"
                    :class="{ 'is-invalid': $v.identification.$error }"
                    placeholder="Type the client's identification"
                  />
                </div>
              </div>
              <div class="col-6">
                <div class="form-group">
                  <label for="name">Name</label>
                  <input
                    type="text"
                    name="name"
                    v-model="name"
                    :class="{ 'is-invalid': $v.name.$error }"
                    class="form-control"
                    placeholder="Type the client's name"
                  />
                </div>
              </div>
              <div class="col-12">
                <div class="form-group">
                  <label for="description">Description</label>
                  <textarea
                    name="description"
                    class="form-control"
                    v-model="description"
                    placeholder="Type the client's description"
                  ></textarea>
                </div>
              </div>
              <div class="col-12">
                <div class="form-group">
                  <address-list :addresses="addresses" />
                </div>
              </div>
            </div>
          </form>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-dismiss="modal">
            Cancel
          </button>
          <button type="button" class="btn btn-success" @click="saveClient()">
            Save changes
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Address from '../shared/Address.vue';
import { validationMixin } from 'vuelidate' 
import { required  } from 'vuelidate/lib/validators'


export default {
  name: "client-form",
  props: {
    title: {
      type: String,
      default: '',
    },
    identification: {
      type: String,
      default: '',
    },
    name: {
      type: String,
      default: '',
    },
    description: {
      type: String,
      default: '',
    },
    addresses: {
      type: Array,
      default: () => [],
    },
  },
  mixins:[validationMixin],
  validations: {
    identification: {
      required,
    },
    name: {
      required
    },
  },
  components: {
      'address-list': Address
  },
  methods: {
    saveClient(){      
      this.$v.$touch()

      if(this.$v.$invalid) return;

      const data = {...this.$data}
      
      this.$emit('saveClient', data)
    }
  }
};
</script>