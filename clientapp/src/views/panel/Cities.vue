<template>
   <div class="card">
      <div class="card-header">
         <div class="d-flex justify-content-between">
            <h4 class="align-self-center">Cities</h4>
            <button type="button" class="btn btn-primary" v-on:click="toggleCreateModal">Create</button>
         </div>
      </div>
      <div class="card-body">
         <table class="table">
            <thead>
            <tr>
               <th scope="col">Id</th>
               <th scope="col">Name</th>
               <th scope="col">Created By</th>
               <th scope="col">Created</th>
               <th scope="col">Actions</th>
            </tr>
            </thead>
            <tbody>
               <tr v-for="city in cities" v-bind:key="city.id">
                  <td scope="row">{{city.id}}</td>
                  <td>{{city.name}}</td>
                  <td>{{city.createdBy}}</td>
                  <td>{{city.created}}</td>
                  <td>
                     <button type="button" class="btn btn-primary mx-2" v-on:click="showModal(city)">
                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-info-circle-fill" viewBox="0 0 16 16">
                           <path d="M8 16A8 8 0 1 0 8 0a8 8 0 0 0 0 16zm.93-9.412l-1 4.705c-.07.34.029.533.304.533.194 0 .487-.07.686-.246l-.088.416c-.287.346-.92.598-1.465.598-.703 0-1.002-.422-.808-1.319l.738-3.468c.064-.293.006-.399-.287-.47l-.451-.081.082-.381 2.29-.287zM8 5.5a1 1 0 1 1 0-2 1 1 0 0 1 0 2z"/>
                        </svg>
                     </button>
                     <button type="button" class="btn btn-danger" v-on:click="showDelete(city)">
                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash-fill" viewBox="0 0 16 16">
                           <path d="M2.5 1a1 1 0 0 0-1 1v1a1 1 0 0 0 1 1H3v9a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V4h.5a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1H10a1 1 0 0 0-1-1H7a1 1 0 0 0-1 1H2.5zm3 4a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 .5-.5zM8 5a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7A.5.5 0 0 1 8 5zm3 .5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 1 0z"/>
                        </svg>
                     </button>
                  </td>
               </tr>
            </tbody>
         </table>
      </div>
   </div>
</template>

<script>
    import {mapActions, mapGetters, mapMutations} from 'vuex';

    export default {
        name: "Cities",
        computed: {
            ...mapGetters('cities', ['cities'])
        },
        methods: {
            ...mapActions('cities', ['fetchCities']),
            ...mapMutations('cities', ['selectCity', 'toggleInfoModal', 'toggleCreateModal', 'toggleDeleteModal']),
            ...mapMutations(['setLoading']),
            showModal(city) {
               this.selectCity({city});
               this.toggleInfoModal();
            },
            showDelete(city){
                this.selectCity({city});
                this.toggleDeleteModal();
            }
        },
        async created() {
            try {
                this.setLoading(true);
                this.fetchCities();
                console.log(this.cities);
            } catch (e) {
                console.log(e);
            }finally {
                this.setLoading(false);
            }
        }
    }
</script>

<style scoped>

</style>
