<template>
   <div class="card">
      <div class="card-header">
         Register
      </div>
      <div class="card-body">
         <form id="register" @action.prevent>
            <div class="form-group">
               <label for="username">Username</label>
               <input type="text" class="form-control" id="username" v-model="username"
                      placeholder="Enter username">
            </div>
            <div class="form-group">
               <label for="email">Email</label>
               <input type="email" class="form-control" id="email" v-model="email"
                      placeholder="Enter email">
            </div>
            <div class="form-group">
               <label for="password">Password</label>
               <input type="password" class="form-control" id="password" v-model="password"
                      placeholder="Password">
            </div>
         </form>
      </div>
      <div class="container" v-if="message !==null">
         <p class="text-info">
            {{message}}
         </p>
      </div>
      <div class="container" v-if="errors !== null">
         <p class="text-danger"> {{errors.Description}}</p>
         <ul v-if="errors.Errors">
            <li class="text-danger" v-for="err in errors.Errors" :key="err.Type">{{err.Type}} | {{err.Description}}</li>
         </ul>
      </div>
      <div class="card-footer d-flex justify-content-between">
         <button form="register" type="submit" class="btn btn-primary" v-on:click="register">Register</button>
         <router-link :to="{name: 'Login'}" class="align-self-center text-decoration-none">Login</router-link>
      </div>
   </div>

</template>

<script>
   import {mapActions} from 'vuex';
    export default {
        name: "RegisterForm",
        data() {
            return{
                username: '',
                email: '',
                password: '',
                errors: null,
                message: null
            }
        },
        methods:{
            ...mapActions(['createAccount']),
            register: async function(event){
                event.preventDefault();
                console.log('register');
                try {
                    await this.createAccount({
                        username: this.username,
                        email: this.email,
                        password: this.password
                    });
                    this.errors = null;
                    this.message = "Your account was register sucessfuly. You will be redirected to login page.";
                    setTimeout(async () => {
                        console.log('timeout');
                     await this.$router.push({path: '/login'});
                    }, 2000);
                }catch (e) {
                    console.log(e);
                    this.errors = e.response.data;
                }
            },
        }
    }
</script>

<style scoped>

</style>
