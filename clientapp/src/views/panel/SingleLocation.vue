<template>
   <div class="card">
      <div class="card-header">
         Location Details
      </div>
      <div class="card-body" v-if="selectedLocation.id === form.id">
         <div class="row">
            <div class="col-md-6">
               <form @submit.prevent>
                  <div class="form-group row mb-1">
                     <label for="id" class="col-sm-2 col-form-label">Id</label>
                     <div class="col-sm-10">
                        <input type="text" disabled class="form-control" id="id" v-model="form.id">
                     </div>
                  </div>
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
                     <label for="city" class="col-sm-2 col-form-label">City</label>
                     <div class="col-sm-10">
                        <select name="city" id="city" v-model="form.city" class="form-control">
                           <option v-for="city in cities" v-bind:value="city.name" v-bind:key="city.id">{{city.name}}</option>
                        </select>
                     </div>
                  </div>
                  <div class="form-group row mb-1">
                     <label for="latitude" class="col-sm-2 col-form-label">Latitude</label>
                     <div class="col-sm-10">
                        <input type="text" class="form-control" id="latitude"
                               v-model="form.latitude">
                     </div>
                  </div>
                  <div class="form-group row mb-1">
                     <label for="longitude" class="col-sm-2 col-form-label">Longitude</label>
                     <div class="col-sm-10">
                        <input type="text" class="form-control" id="longitude"
                               v-model="form.longitude">
                     </div>
                  </div>
                  <div class="form-group row mb-1">
                     <label for="createdBy" class="col-sm-2 col-form-label">Created By</label>
                     <div class="col-sm-10">
                        <input type="text" disabled class="form-control" id="createdBy"
                               v-model="selectedLocation.createdBy">
                     </div>
                  </div>
                  <div class="form-group row mb-1">
                     <label for="created" class="col-sm-2 col-form-label">Created</label>
                     <div class="col-sm-10">
                        <input type="text" disabled class="form-control" id="created"
                               v-model="selectedLocation.created">
                     </div>
                  </div>
                  <div class="form-group row mb-1">
                     <label for="modified" class="col-sm-2 col-form-label">Modified</label>
                     <div class="col-sm-10">
                        <input type="text" disabled class="form-control" id="modified"
                               v-model="selectedLocation.lastModified">
                     </div>
                  </div>
                  <div class="form-group row mb-1">
                     <label for="lastModifiedBy" class="col-sm-2 col-form-label">Modified By</label>
                     <div class="col-sm-10">
                        <input type="text" disabled class="form-control" id="lastModifiedBy"
                               v-model="selectedLocation.lastModifiedBy">
                     </div>
                  </div>
                  <div class="container" v-if="errors !== null">
                     <p class="text-danger"> {{errors.Description}}</p>
                     <ul v-if="errors.Errors">
                        <li class="text-danger" v-for="err in errors.Errors" :key="err.Type">{{err.Type}} | {{err.Description}}</li>
                     </ul>
                  </div>
                  <div v-if="message">
                     <p class="text-success">{{message}}</p>
                  </div>
               </form>
            </div>
            <div class="col-md-6">
               <div id="map" ref="map"></div>
            </div>
         </div>
      </div>
      <div class="card-footer">
         <button type="button" v-on:click="back" class="btn btn-secondary">Back to List</button>
         <button type="button" v-on:click="update" class="btn btn-info mx-2">Save Changes</button>
      </div>
   </div>
</template>

<script>
    import {mapActions, mapGetters, mapMutations} from 'vuex';

    export default {
        name: "SingleLocation",
        data() {
            return{
                map: null,
                marker: null,
                form: {
                    id: null,
                    name: '',
                    address: '',
                    city: '',
                    latitude: '',
                    longitude: ''
                },
                errors: null,
                message: false,
            }
        },
        computed: {
            ...mapGetters('locations', ['selectedLocation']),
            ...mapGetters('cities', ['cities'])
        },
        methods: {
            ...mapActions('locations', ['fetchLocation', 'updateLocation']),
            ...mapActions('cities', ['fetchCities']),
            ...mapMutations(['incrementKey', 'setLoading']),
            back: function () {
                this.$router.push({name:'Locations'});
            },
            update: async function(){
                try {
                    this.setLoading(true);
                    await this.updateLocation({form: this.form});
                    this.errors = null;
                    this.message = "Changes were successfully saved!";
                    //this.setMarkers(this.longitude, this.latitude);

                    setTimeout(() =>{
                        this.message = false;
                    }, 1500);
                    await this.$router.push({path: this.$route.fullPath});
                    this.incrementKey();
                }catch (e) {
                    console.log(e);
                    this.errors = e.response.data;
                }finally {
                    this.setLoading(false);
                }
            },
            setMarkers: function (lng, lat) {
                this.map = null;
                this.marker = null;
                this.map = new window.google.maps.Map(this.$refs["map"], {
                    center: {
                        lng: lng,
                        lat: lat
                    },
                    zoom: 10
                });
                this.marker = new window.google.maps.Marker({
                    position: {
                        lng: lng,
                        lat: lat,
                    },
                    map: this.map,
                    title: "Hello World!",
                });
            }
        },
        async created() {
            this.form.id = this.$route.params.id;
            this.setLoading(true);
            await this.fetchLocation({id: this.form.id});
            await this.fetchCities();

            this.setMarkers(this.selectedLocation.longitude, this.selectedLocation.latitude);

            this.form.name = this.selectedLocation.name;
            this.form.address = this.selectedLocation.address;
            this.form.city = this.selectedLocation.city.name;
            this.form.latitude = this.selectedLocation.latitude;
            this.form.longitude = this.selectedLocation.longitude;
            this.setLoading(false);
        },
    }

</script>

<style scoped>
   #map {
      min-height: 300px;
      height: 100%;
   }
</style>
