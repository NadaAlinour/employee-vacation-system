<template>
    <div class="main-container">
        <header>

            <div class="wrapper">
                <NavbarItem v-if="!this.$store.state.email"></NavbarItem>



                <div v-if="this.$store.state.email && this.$route.path !='/404'">
                    <SidebarItem></SidebarItem>
                </div>


            </div>
        </header>
        <div class="main-content">
            <RouterView />
        </div>
    </div>


</template>

<script>
    import { RouterLink, RouterView } from 'vue-router'
    import NavbarItem from './components/NavbarItem.vue'
    import SidebarItem from './components/SidebarItem.vue'

   

    export default {
        components: {
            NavbarItem,
            SidebarItem,
            RouterView
        },

    }


</script>

<style scoped>
</style>
