<template>
   <div class="modal fade" v-bind:class="{'show': showCreate}" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
      <div class="modal-dialog" role="document">
         <div class="modal-content">
            <div class="modal-header">
               <h5 class="modal-title" id="exampleModalLabel">Create City</h5>
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
               <button type="button" class="btn btn-primary" v-on:click="createCity" >Create</button>
            </div>
         </div>
      </div>
   </div>

</template>

<script>
   import {mapGetters, mapActions, mapMutations} from 'vuex';

    export default {
        name: "CreateCity",
        data() {
            return{
                form: {
                    name: '',
                },
                errors: null,
            }
        },
        computed: {
            ...mapGetters('cities',['showCreate'])
        },
        methods: {
            ...mapActions('cities', ['submitForm']),
            ...mapMutations('cities', ['toggleCreateModal']),
            ...mapMutations(['setLoading']),
            createCity: async function(){
                try {
                    this.setLoading(true);
                    await this.submitForm({form: this.form});
                    this.toggleCreateModal();
                }catch (e) {
                    this.errors = e.response.data;
                }finally {
                    this.setLoading(false);
                }
            }
        }
    }
</script>

<style scoped>
   .show {
      display: block;
   }
</style>
