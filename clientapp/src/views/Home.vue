<template>
   <section id="home">
      <div class="container">
         <nav class="navbar navbar-expand-lg navbar-light bg-light justify-content-between mb-5">
            <ul class="navbar-nav mr-auto mt-2 mt-lg-0">
               <li class="nav-item">
                  <router-link class="nav-link" :to="{name: 'Locations' }">Locations</router-link>
               </li>
               <li class="nav-item">
                  <router-link class="nav-link" :to="{name: 'Cities' }">Cities</router-link>
               </li>
               <li class="nav-item">
                  <router-link class="nav-link" :to="{name: 'UserInfo' }">User Info</router-link>
               </li>
            </ul>
            <button class="btn btn-outline-danger mx-5" type="button" v-on:click="logout">Logout</button>
         </nav>
         <router-view :key="componentKey" />
      </div>
   </section>
</template>

<script>
    import {mapMutations, mapGetters} from "vuex";

    export default {
        name: 'Home',
        computed: {
            ...mapGetters(["componentKey"]),
        },
        components: {},
        methods: {
            ...mapMutations(["resetState"]),
            logout: async function () {
                console.log('clicked');
                this.resetState();
                await this.$router.push('login');
            }
        }
    }
</script>

<style lang="scss">
   #home {

   }
</style>
