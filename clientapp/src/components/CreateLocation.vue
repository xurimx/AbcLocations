<template>
   <div class="modal fade" v-bind:class="{'show': showCreate}" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
      <div class="modal-dialog" role="document">
         <div class="modal-content">
            <div class="modal-header">
               <h5 class="modal-title" id="exampleModalLabel">Create Location</h5>
               <button type="button" class="close" data-dismiss="modal" aria-label="Close" @click="$emit('close')">
                  <span aria-hidden="true">&times;</span>
               </button>
            </div>
            <div class="modal-body">
               <form class="mb-1">
                  <div class="form-group row mb-1">
                     <label for="name" class="col-sm-2 col-form-label">Name</label>
                     <div class="col-sm-10">
                        <input type="text" class="form-control" id="name" v-model="form.name">
                     </div>
                  </div>
                  <div class="form-group row mb-1">
                     <label for="address" class="col-sm-2 col-form-label">Address</label>
                     <div class="col-sm-10">
                        <input type="text" class="form-control" id="address" v-model="form.address">
                     </div>
                  </div>
                  <div class="form-group row mb-1">
                     <label for="latitude" class="col-sm-2 col-form-label">Latitude</label>
                     <div class="col-sm-10">
                        <input type="number" class="form-control" id="latitude" v-model="form.latitude">
                     </div>
                  </div>
                  <div class="form-group row mb-1">
                     <label for="longitude" class="col-sm-2 col-form-label">Longitude</label>
                     <div class="col-sm-10">
                        <input type="number" class="form-control" id="longitude" v-model="form.longitude">
                     </div>
                  </div>
                  <div class="form-group row mb-1">
                     <label for="city" class="col-sm-2 col-form-label">City</label>
                     <div class="col-sm-10">
                        <select name="city" id="city" v-model="form.city" class="form-control">
                           <option value="" selected>--Select City--</option>
                           <option v-for="city in cities" v-bind:value="city.name" v-bind:key="city.id">{{city.name}}</option>
                        </select>
                     </div>
                  </div>
                  <div class="container" v-if="errors !== null">
                     <p class="text-danger"> {{errors.Description}}</p>
                     <ul v-if="errors.Errors">
                        <li class="text-danger" v-for="err in errors.Errors" :key="err.Type">{{err.Type}} | {{err.Description}}</li>
                     </ul>
                  </div>
               </form>
            </div>
            <div class="modal-footer">
               <button type="button" class="btn btn-secondary" @click="$emit('close')">Close</button>
               <button type="button" class="btn btn-primary" v-on:click="createLocation" >Create</button>
            </div>
         </div>
      </div>
   </div>

</template>

<script>
   import {mapGetters, mapActions, mapMutations} from 'vuex';

    export default {
        name: "CreateLocation",
        data() {
            return{
                form: {
                    name: '',
                    address: '',
                    longitude: 0,
                    latitude: 0,
                    city: ''
                },
                errors: null,
            }
        },
        computed: {
            ...mapGetters('cities',['cities']),
            ...mapGetters('locations',['showCreate'])
        },
        methods: {
            ...mapActions('locations', ['submitForm']),
            ...mapActions('cities', ['fetchCities']),
            ...mapMutations('locations', ['toggleCreateModal']),
            createLocation: async function(){
                try {
                    await this.submitForm({form: this.form});
                    this.toggleCreateModal();
                }catch (e) {
                    console.log(e);
                    this.errors = e.response.data;
                }
            }
        },
        async mounted() {
            await this.fetchCities();
        }
    }
</script>

<style scoped>
   .show {
      display: block;
   }
</style>
