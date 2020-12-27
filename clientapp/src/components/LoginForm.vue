<template>
   <div class="card">
      <div class="card-header">
         Login
      </div>
      <div class="card-body">
         <form id="login"  @action.prevent>
            <div class="form-group">
               <label for="username">Username</label>
               <input type="text" class="form-control" id="username" v-model="username"
                      placeholder="Enter username">
            </div>
            <div class="form-group">
               <label for="password">Password</label>
               <input type="password" class="form-control" id="password" v-model="password"
                      placeholder="Password">
            </div>
         </form>
      </div>
      <div class="container" v-if="errors !== null">
            <p class="text-danger"> {{errors.Description}}</p>
            <ul v-if="errors.Errors">
               <li class="text-danger" v-for="err in errors.Errors" :key="err.Type">{{err.Type}} | {{err.Description}}</li>
            </ul>
      </div>
      <div class="card-footer d-flex justify-content-between">
         <button form="login" v-on:click="login" type="submit" class="btn btn-primary">Login</button>
         <router-link to="/register" class="align-self-center text-decoration-none">Register</router-link>
      </div>
   </div>
</template>

<script>
   import {mapActions} from 'vuex';
    export default {
        name: "LoginForm",
        data() {
            return{
              username: '',
              password: '',
              errors: null,
            }
        },
        methods: {
            ...mapActions(['authenticate', 'userinfo']),
            login: async function (event) {
                event.preventDefault();
                console.log('clicked');
                try {
                    await this.authenticate({
                        username: this.username,
                        password: this.password
                    });
                    await this.userinfo();
                    await this.$router.push('/');
                }catch (e) {
                    console.log(e.response);
                    this.errors = e.response.data;
                }
            },
        }
    }
</script>

<style scoped>

</style>
