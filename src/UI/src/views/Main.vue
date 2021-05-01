<template>
    <v-layout>
        <ChatModule />
        <VehiclePanel />
        <DashboardPanel />
    </v-layout>
</template>

<style scoped>
    .home {
        background: none;
    }
</style>

<script lang="ts">
    import ChatModule from '../components/MessageBox/Main.vue';
    import VehiclePanel from '../components/VehiclePanel/Main.vue';
    import DashboardPanel from '../components/VehicleDashboard/Main.vue';
    import { Component, Vue } from 'vue-property-decorator';

    @Component({
        components: {
            ChatModule,
            VehiclePanel,
            DashboardPanel
        },
    })
    export default class Home extends Vue { }
</script>
