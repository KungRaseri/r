<template>
    <v-layout>
        <ChatModule />
        <VehiclePanel />
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
    import { Component, Vue } from 'vue-property-decorator';

    @Component({
        components: {
            ChatModule,
            VehiclePanel
        },
    })
    export default class Home extends Vue { }
</script>
