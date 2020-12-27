<template>
   <section id="login">
      <div class="container">
         <div class="form">
            <div class="row">
               <div class="col-lg-6 offset-lg-3 col-md-8 offset-md-2 col-sm-12 offset-sm-0">
                  <login-form></login-form>
               </div>
            </div>
         </div>
      </div>
   </section>
</template>

<script>
   import {mapState} from 'vuex';
    import LoginForm from "../components/LoginForm";
    export default {
        name: "Register",
        components: {LoginForm},
        computed: {
            ...mapState(['role'])
        }
    }
</script>

<style scoped lang="scss">
   #login{
      .form{
         padding-top: 200px;
      }
   }
</style>
