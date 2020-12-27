<template>
   <div class="card align-self-center mx-auto" style="width: 18rem;">
      <div class="card-body">
         <h5 class="card-title">User details</h5>
      </div>
      <ul class="list-group list-group-flush">
         <li class="list-group-item">User id: {{user.id}}</li>
         <li class="list-group-item">Email: {{user.email}}</li>
         <li class="list-group-item">Username: {{user.username}}</li>
         <li class="list-group-item">Role: {{user.role}}</li>
      </ul>
   </div>
</template>

<script>
    import {mapGetters} from "vuex";

    export default {
        name: "UserInfo",
        computed:{
            ...mapGetters(['user']),
        }
    }
</script>

<style scoped>

</style>
