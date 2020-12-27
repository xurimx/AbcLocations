<template>
   <div class="modal fade" v-bind:class="{'show': showInfo}" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
      <div class="modal-dialog modal-lg" role="document">
         <div class="modal-content">
            <div class="modal-header">
               <h5 class="modal-title" id="exampleModalLabel">City Details</h5>
               <button type="button" class="close" data-dismiss="modal" aria-label="Close" @click="$emit('close')">
                  <span aria-hidden="true">&times;</span>
               </button>
            </div>
            <div class="modal-body">
               <form>
                  <div class="form-group row mb-1">
                     <label for="id" class="col-sm-2 col-form-label">Id</label>
                     <div class="col-sm-10">
                        <input type="text" disabled class="form-control" id="id" v-model="form.id">
                     </div>
                  </div>
                  <div class="form-group row mb-1">
                     <label for="id" class="col-sm-2 col-form-label">Name</label>
                     <div class="col-sm-10">
                        <input type="text" disabled class="form-control" id="name" v-model="form.name">
                     </div>
                  </div>
                  <div class="form-group row mb-1">
                     <label for="id" class="col-sm-2 col-form-label">Created By</label>
                     <div class="col-sm-10">
                        <input type="text" disabled class="form-control" id="createdBy" v-model="form.createdBy">
                     </div>
                  </div>
                  <div class="form-group row mb-1">
                     <label for="id" class="col-sm-2 col-form-label">Created</label>
                     <div class="col-sm-10">
                        <input type="text" disabled class="form-control" id="created" v-model="form.created">
                     </div>
                  </div>
               </form>
            </div>
            <div class="modal-footer">
               <button type="button" class="btn btn-secondary" @click="$emit('close')">Close</button>
               <button type="button" class="btn btn-primary" @click="$emit('close')" >Save changes</button>
            </div>
         </div>
      </div>
   </div>
</template>

<script>
   import {mapGetters} from 'vuex';
    export default {
        name: "CityDetails",
        data() {
            return{
                form: {
                    id: '',
                    name: '',
                    createdBy: '',
                    created: '',
                    lastModifiedBy: '',
                    lastModified: ''
                },
                errors: null,
            }
        },
        computed:{
            ...mapGetters('cities', ['showInfo', 'selectedCity'])
        },
        mounted() {
            this.form.id = this.selectedCity.id;
            this.form.name = this.selectedCity.name;
            this.form.createdBy = this.selectedCity.createdBy;
            this.form.created = this.selectedCity.created;
            this.form.lastModifiedBy = this.selectedCity.lastModifiedBy;
            this.form.lastModified = this.selectedCity.lastModified;
        }
    }
</script>

<style scoped>
   .show {
      display: block;
   }

</style>
