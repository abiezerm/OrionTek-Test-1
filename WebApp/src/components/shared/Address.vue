<template>
  <div class="row">
    <div class="col-12">
      <div class="row mb-2">
        <div class="col-6">
          <h5>List of addresses</h5>
        </div>
        <div class="col-6">
          <div class="float-right">
            <button
              type="button"
              class="btn btn-sm btn-success"
              @click="
                () => {
                  openForm = true;
                }
              "
            >
              Add address
            </button>
          </div>
        </div>
      </div>
      <ul class="list-group" v-if="addresses && addresses.length > 0">
        <li
          class="list-group-item"
          v-for="(item, index) in addresses"
          :key="index"
          @click="editAddress(index)"
        >
          <div class="row">
            <div class="col-8">
              {{ item.number }} {{ item.streetName }} <br />
              {{ item.city }} {{ item.state }} <br />
              {{ item.zipCode }}
            </div>
            <div class="col-4">
              <button
                type="button"
                title="Delete address"
                class="close"
                @click="
                  () => {
                    addresses.splice(index, 1);
                  }
                "
              >
                <span aria-hidden="true">&times;</span>
              </button>
            </div>
          </div>
        </li>
      </ul>
      <p v-else class="text-center">No data to show</p>
      <br />
    </div>
    <div class="col-12" v-if="openForm">
      <div class="row">
        <div class="col-4">
          <div class="form-group">
            <label for="number">Number</label>
            <input
              type="text"
              placeholder="500A"
              v-model="number"
              maxlength="5"
              class="form-control form-control-sm"
              :class="{ 'is-invalid': $v.number.$error }"
              name="number"
            />
          </div>
        </div>
        <div class="col-8">
          <div class="form-group">
            <label for="streetName">StreetName</label>
            <input
              type="text"
              placeholder="Example street"
              v-model="streetName"
              class="form-control form-control-sm"
              :class="{ 'is-invalid': $v.streetName.$error }"
              name="streetName"
            />
          </div>
        </div>
        <div class="col-3">
          <div class="form-group">
            <label for="city">City</label>
            <input
              type="text"
              class="form-control form-control-sm"
              :class="{ 'is-invalid': $v.city.$error }"
              placeholder="Example city"
              v-model="city"
              name="city"
            />
          </div>
        </div>
        <div class="col-3">
          <div class="form-group">
            <label for="state">State</label>
            <input
              type="text"
              class="form-control form-control-sm"
              :class="{ 'is-invalid': $v.state.$error }"
              name="state"
              v-model="state"
              placeholder="Example state"
            />
          </div>
        </div>
        <div class="col-3">
          <div class="form-group">
            <label for="zipCode">ZipCode</label>
            <input
              type="text"
              class="form-control form-control-sm"
              :class="{ 'is-invalid': $v.zipCode.$error }"
              name="zipCode"
              maxlength="9"
              v-model="zipCode"
              placeholder="00000-0000"
            />
          </div>
        </div>
        <div class="col-3">
          <div class="form-group mt-5">
            <button
              type="button"
              @click="saveAddress()"
              class="btn btn-success btn-sm"
            >
              Save address
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { validationMixin } from "vuelidate";
import { required } from "vuelidate/lib/validators";

export default {
  name: "address-list",
  props: {
    addresses: {
      type: Array,
      default: () => [],
    },
  },
  mixins: [validationMixin],
  data() {
    return {
      openForm: false,
      currentAddressIndex: null,
      streetName: "",
      number: "",
      city: "",
      state: "",
      zipCode: "",
    };
  },
  validations: {
    streetName: {
      required,
    },
    number: {
      required,
    },
    state: {
      required,
    },
    city: {
      required,
    },
    zipCode: {
      required,
    },
  },
  methods: {
    saveAddress() {
      if (this.$data.currentAddressIndex) this.updateAddress();
      else this.addAddress();
    },
    addAddress() {
      this.$v.$touch();

      if (this.$v.$invalid) return;

      this.addresses.push({
        streetName: this.$data.streetName,
        number: this.$data.number,
        city: this.$data.city,
        state: this.$data.state,
        zipCode: this.$data.zipCode,
      });

      this.$data.openForm = false;
      this.clearForm();
    },
    editAddress(index) {
      this.$data.streetName = this.addresses[index].streetName;
      this.$data.number = this.addresses[index].number;
      this.$data.city = this.addresses[index].number;
      this.$data.state = this.addresses[index].state;
      this.$data.zipCode = this.addresses[index].zipCode;
      this.$data.openForm = true;
      this.$data.currentAddressIndex = index;
    },
    updateAddress() {
      this.$v.$touch();

      if (this.$v.$invalid) return;

      this.addresses[this.$data.currentAddressIndex].streetName =
        this.$data.streetName;
      this.addresses[this.$data.currentAddressIndex].number = this.$data.number;
      this.addresses[this.$data.currentAddressIndex].number = this.$data.city;
      this.addresses[this.$data.currentAddressIndex].state = this.$data.state;
      this.addresses[this.$data.currentAddressIndex].zipCode =
        this.$data.zipCode;

      this.$data.openForm = false;
      this.clearForm();
    },
    clearForm() {
      this.$data.streetName = "";
      this.$data.number = "";
      this.$data.city = "";
      this.$data.state = "";
      this.$data.zipCode = "";
      this.$data.currentAddressIndex = null;
    },
  },
};
</script>