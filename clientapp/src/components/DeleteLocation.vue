<template>
   <div class="modal fade" v-bind:class="{'show': showDelete}" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
      <div class="modal-dialog" role="document">
         <div class="modal-content">
            <div class="modal-header">
               <h5 class="modal-title" id="exampleModalLabel">Are you sure you want to delete this Location?</h5>
               <button type="button" class="close" data-dismiss="modal" aria-label="Close" @click="$emit('close')">
                  <span aria-hidden="true">&times;</span>
               </button>
            </div>
            <div class="modal-body">
               <form class="mb-1">
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
               </form>
            </div>
            <div class="modal-footer">
               <button type="button" class="btn btn-secondary" @click="$emit('close')">Close</button>
               <button type="button" class="btn btn-primary" v-on:click="deleteSelected">Delete</button>
            </div>
         </div>
      </div>
   </div>

</template>

<script>
    import {mapGetters, mapActions, mapMutations} from "vuex";

    export default {
        name: "DeleteLocation",
        data() {
            return{
                form: {
                    id: '',
                    name: '',
                },
            }
        },
        computed:{
            ...mapGetters('locations', ['showDelete', 'selectedLocation']),
        },
        methods:{
            ...mapActions('locations', ['deleteLocation', "fetchLocations"]),
            ...mapMutations('locations', ['toggleDeleteModal']),
            ...mapMutations(['setLoading']),

            deleteSelected: async function () {
                try {
                    this.setLoading(true);
                    await this.deleteLocation({id: this.form.id});
                    this.toggleDeleteModal();
                    await this.$router.push({name: 'Locations'});
                    //await this.fetchLocations();
                }catch (e) {
                    console.log(e.response);
                }finally {
                    this.setLoading(false);
                }
            }
        },
        mounted() {
            this.form.id = this.selectedLocation.id;
            this.form.name = this.selectedLocation.name;
        }
    }
</script>

<style scoped>
   .show {
      display: block;
   }
</style>
