<template>
    <v-app app>
        <router-view />
    </v-app>
</template>

<style>
    * {
        font-family: 'Unica One', cursive;
    }

    html {
        background: none transparent;
        width: 100%;
        height: 100%;
        overflow-x: hidden;
        overflow-y: hidden;
    }

    /* Let's get this party started */
    ::-webkit-scrollbar {
        width: 0px;
    }

    /* Track */
    ::-webkit-scrollbar-track {
        -webkit-box-shadow: inset 0 0 6px rgba(0, 0, 0, 0.3);
        box-shadow: inset 0 0 6px rgba(0, 0, 0, 0.3);
        -webkit-border-radius: 10px;
        border-radius: 10px;
    }

    /* Handle */
    ::-webkit-scrollbar-thumb {
        -webkit-border-radius: 10px;
        border-radius: 10px;
        background: rgba(183, 28, 28, 0.8);
        -webkit-box-shadow: inset 0 0 6px rgba(0, 0, 0, 0.5);
        box-shadow: inset 0 0 6px rgba(0, 0, 0, 0.5);
    }

        ::-webkit-scrollbar-thumb:window-inactive {
            background: rgba(183, 28, 28, 0.8);
        }

    ::-webkit-scrollbar-corner {
        background: transparent;
        border: none;
    }

    ::-webkit-resizer {
        background: transparent;
        border: none;
    }

    .theme--dark.v-application {
        background: none !important;
    }
</style>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';

    @Component({
        components: {}
    })
    export default class App extends Vue { }
</script>