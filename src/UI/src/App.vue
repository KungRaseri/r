<template>
    <v-app app>
        <v-main>
            <router-view />
        </v-main>
    </v-app>
</template>

<style>
    html {
        background: none transparent;
        width: 100%;
        height: 100%;
        overflow-x: hidden;
        overflow-y: hidden;
    }

    .v-application {
        background: none !important;
    }
</style>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';

    @Component({
        components: {}
    })
    export default class App extends Vue {

        mounted() {
            console.log("[OpenRP] Application has been mounted.")
        }
    }
</script>