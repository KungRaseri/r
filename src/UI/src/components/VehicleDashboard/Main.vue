<template>
    <v-card class="panel" color="rgba(0, 0, 0, 0.5)">
        <v-container>
            <v-row justify="center">
                <SpeedGuage class="mr-1" />
                <v-container class="turn-signal">
                    <v-row justify="center">
                        <TurnSignal :id="1" icon="mdi-menu-left"/>
                        <TurnSignal :id="2" icon="mdi-menu-right" />
                    </v-row>
                </v-container>
            <RpmGuage class="ml-1" />
        </v-row>
    </v-container>
</v-card>
</template>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';
    import SpeedGuage from './SpeedGuage.vue';
    import RpmGuage from './RpmGuage.vue';
    import TurnSignal from './TurnSignal.vue';

    @Component({
        components: {
            SpeedGuage,
            RpmGuage,
            TurnSignal
        },
    })
    export default class DashboardPanel extends Vue {
    }
</script>

<style>
    html {
        --dashboard-panel-width: 25%;
    }

    .fast-transition .v-progress-circular__overlay {
        transition: all 0.1s ease-in-out;
    }

    .guage-label {
        font-size: calc((1vw + 1vh));
    }

    .guage-sub-label {
        font-size: calc((1vw + 1vh) * 0.4);
        margin-top: calc(1vh * 0.3) !important;
    }

    .turn-signal {
        width: auto;
        margin: 0;
    }
</style>

<style scoped>
    .panel {
        position: fixed;
        width: var(--dashboard-panel-width);
        height: auto;
        bottom: 1%;
        margin-left: calc((100% - var(--dashboard-panel-width)) / 2 );
        margin-right: calc((100% - var(--dashboard-panel-width)) / 2);
        font-weight: bold;
        color: white;
        z-index: 50;
    }
</style>