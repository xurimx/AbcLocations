<template>
  <router-view :key="$route.path"/>
  <city-details v-if="showInfo" @close="toggleInfoModal"></city-details>
  <create-city v-if="showCreate" @close="toggleCreateModal"></create-city>
  <delete-city v-if="showDelete" @close="toggleDeleteModal"></delete-city>
  <create-location v-if="showCreateLoc" @close="toggleCreateModalLoc"></create-location>
  <delete-location v-if="showDeleteLoc" @close="toggleDeleteModalLoc"></delete-location>
  <spinner v-if="loading"></spinner>
</template>

<script>
  import {mapActions, mapGetters, mapMutations} from 'vuex';
  import CityDetails from "./components/CityDetails";
  import CreateCity from "./components/CreateCity";
  import DeleteCity from "./components/DeleteCity";
  import CreateLocation from "./components/CreateLocation";
  import DeleteLocation from "./components/DeleteLocation";
  import Spinner from "./components/Spinner";

  export default {
    components: {Spinner, CreateLocation, DeleteCity, CreateCity, CityDetails, DeleteLocation},
    async beforeCreate() {
      this.$store.commit('initialiseStore');
    },
    async created() {
      let initPath = window.location.pathname;
       try {
         await this.$store.dispatch('userinfo');
         if (this.role === 'guest' || null){
          await this.$router.push({name:'Login'});
        }else{
           // console.log('not guest', window.location);
          await this.$router.push({path: initPath});
        }
      }catch (e) {
        await this.$router.push({name:'Login'});
      }
    },
    computed: {
      ...mapGetters(['role', 'loading']),
      ...mapGetters('cities', ['showInfo', 'showCreate', 'showDelete']),
      ...mapGetters('locations', {'showCreateLoc':'showCreate', 'showDeleteLoc': 'showDelete'})
    },
    methods: {
      ...mapMutations('cities', ['toggleInfoModal', 'toggleCreateModal', 'toggleDeleteModal']),
      ...mapMutations('locations', {'toggleCreateModalLoc':'toggleCreateModal', 'toggleDeleteModalLoc':'toggleDeleteModal'}),
      ...mapActions(['userinfo']),
    }
  }
</script>

<style lang="scss">
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
}
</style>
