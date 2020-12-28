<template>
   <div class="card">
      <div class="card-header">
         <div class="d-flex">
            <h4 class="align-self-center">Locations</h4>
            <div class="d-flex col-md-4 offset-md-7">
               <select class="form-control mx-2 w-25" v-model="limit">
                  <option value="10" selected>10</option>
                  <option value="15">15</option>
                  <option value="20">20</option>
               </select>
               <input type="text" class="form-control" v-model="search">
               <button type="button" class="btn btn-primary mx-2" v-on:click="toggleCreateModal">Create</button>
            </div>
         </div>
      </div>
      <div class="card-body">
         <table class="table">
            <thead>
            <tr>
               <th scope="col">Id</th>
               <th scope="col">Name</th>
               <th scope="col">Address</th>
               <th scope="col">City</th>
               <th scope="col">Created By</th>
               <th scope="col">Actions</th>
            </tr>
            </thead>
            <tbody>
            <tr v-for="location in locations" v-bind:key="location.id">
               <td scope="row">{{location.id}}</td>
               <td>{{location.name}}</td>
               <td>{{location.address}}</td>
               <td>{{location.city.name}}</td>
               <td>{{location.createdBy}}</td>
               <td>
                  <router-link :to="{name: 'SingleLocation', params:{id: location.id}}" class="btn btn-primary mx-2">
                     <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-info-circle-fill" viewBox="0 0 16 16">
                     <path d="M8 16A8 8 0 1 0 8 0a8 8 0 0 0 0 16zm.93-9.412l-1 4.705c-.07.34.029.533.304.533.194 0 .487-.07.686-.246l-.088.416c-.287.346-.92.598-1.465.598-.703 0-1.002-.422-.808-1.319l.738-3.468c.064-.293.006-.399-.287-.47l-.451-.081.082-.381 2.29-.287zM8 5.5a1 1 0 1 1 0-2 1 1 0 0 1 0 2z"/>
                     </svg>
                  </router-link>
                  <button type="button" class="btn btn-danger" v-on:click="showDelete(location)">
                     <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash-fill" viewBox="0 0 16 16">
                        <path d="M2.5 1a1 1 0 0 0-1 1v1a1 1 0 0 0 1 1H3v9a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V4h.5a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1H10a1 1 0 0 0-1-1H7a1 1 0 0 0-1 1H2.5zm3 4a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 .5-.5zM8 5a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7A.5.5 0 0 1 8 5zm3 .5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 1 0z"/>
                     </svg>
                  </button>
               </td>
            </tr>
            </tbody>
         </table>
      </div>
      <div class="card-footer">
         <nav aria-label="Page navigation example">
            <ul class="pagination justify-content-end">
               <li class="page-item" :class="{'disabled' : pagination.currentPage == 1}">
                  <button class="page-link" :disabled="pagination.currentPage == 1" v-on:click="gotoPage(1)">First</button>
               </li>
               <li class="page-item" :class="{'disabled' : pagination.currentPage == 1}">
                  <button class="page-link" :disabled="pagination.currentPage == 1" v-on:click="gotoPage(pagination.currentPage - 1)">Prev</button>
               </li>
               <li class="page-item page-link">{{pagination.currentPage}}</li>
               <li class="page-item" :class="{'disabled' : pagination.currentPage == pagination.totalPages}">
                  <button class="page-link" :disabled="pagination.currentPage == pagination.totalPages" v-on:click="gotoPage(pagination.currentPage + 1)" >Next</button>
               </li>
               <li class="page-item" :class="{'disabled' : pagination.currentPage == pagination.totalPages}">
                  <button class="page-link" :disabled="pagination.currentPage == pagination.totalPages" v-on:click="gotoPage(pagination.totalPages)">Last</button>
               </li>
            </ul>
         </nav>
      </div>
   </div>
</template>

<script>
    import {mapActions, mapGetters, mapMutations} from "vuex";

    export default {
        name: "Locations",
        data: () => ({
        }),
        computed: {
            ...mapGetters('locations', ['locations', 'pagination', 'query']),
            limit: {
                get(){
                    return this.query.limit;
                },
                set(val){
                    let query = this.query;
                    query.limit = val;
                    query.page = 1;
                    this.setQuery({query});
                    this.fetch();
                }
            },
            search: {
              get(){
                  return this.query.search;
              },
              set(val){
                  let query = this.query;
                  query.page = 1;
                  query.search = val;
                  this.setQuery({query});
                  if (val.length >= 3){
                      this.fetch();
                  }
                  if (val.length === 0){
                      this.fetch();
                  }
                }
            },
        },
        methods:{
            ...mapActions('locations', ['fetchLocations']),
            ...mapMutations('locations', ['toggleCreateModal', 'toggleDeleteModal','setQuery', 'selectLocation']),
            ...mapMutations(['setLoading']),
            fetch: async function () {
                this.setLoading(true);
                await this.fetchLocations();
                this.setLoading(false);
            },
            gotoPage: async function(page){
                let query = this.query;
                query.page = page;
                this.setQuery({query});
                await this.fetch();
            },
            showDelete(location){
                console.log(location);
                this.selectLocation({location});
                this.toggleDeleteModal();
            }
        },
        async created() {
            try {
               await this.fetch();
            } catch (e) {
                console.log(e);
            }
        }
    }
</script>

<style scoped>

</style>
