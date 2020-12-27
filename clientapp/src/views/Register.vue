<template>
   <section id="register">
      <div class="container">
         <div class="form">
            <div class="row">
               <div class="col-lg-6 offset-lg-3 col-md-8 offset-md-2 col-sm-12 offset-sm-0">
                  <register-form></register-form>
               </div>
            </div>
         </div>
      </div>
   </section>
</template>

<script>
    import RegisterForm from "../components/RegisterForm";
    export default {
        name: "Register",
        components: {RegisterForm}
    }
</script>

<style scoped lang="scss">
   #register{
      .form{
         padding-top: 200px;
      }
   }
</style>
