<template>
    <v-card v-show="IsDashboardPanelActive" class="panel" color="rgba(0, 0, 0, 0.5)" rounded="pill">
        <v-container>
            <v-row justify="center">
                <SpeedGuage class="mr-1 mt-2 mb-2" />
                <v-container class="turn-signal">
                    <v-row justify="center">
                        <TurnSignal :id="1" icon="mdi-menu-left"/>
                        <TurnSignal :id="2" icon="mdi-menu-right" />
                    </v-row>
                    <v-row justify="center">
                        <v-icon :color="LightStatus()" size="35">mdi-car-light-high</v-icon>
                    </v-row>
                </v-container>
            <RpmGuage class="ml-1 mt-2 mb-2" />
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
        isDashboardPanelActive = false;
        lights = false;
        highbeams = false;

        mounted() {
            window.addEventListener("message", (e) => {
                switch (e.data.eventName) {
                    case "TOGGLE_DASHBOARD_PANEL_MODULE":
                        this.IsDashboardPanelActive = e.data.visible;
                        break;
                    case "VEHICLE_LIGHTS_MONITOR":
                        this.PanelStatus(e.data);
                        break;
                    default:
                        break;
                }
            });
        }

        PanelStatus(value: any) {
            if (this.Lights != value._lights) {
                this.Lights = value._lights;
            }

            if (this.Highbeams != value._highbeams) {
                this.Highbeams = value._highbeams;
            }
        }

        LightStatus() {
            if (this.Highbeams) {
                return "light-blue lighten-2";
            } else if (this.Lights) {
                return "white";
            }

            return "black";
        }

        get IsDashboardPanelActive() {
            return this.isDashboardPanelActive
        }

        set IsDashboardPanelActive(value: boolean) {
            this.isDashboardPanelActive = value;
        }

        get Lights() {
            return this.lights;
        }

        set Lights(value: boolean) {
            this.lights = value;
        }

        get Highbeams() {
            return this.highbeams;
        }

        set Highbeams(value: boolean) {
            this.highbeams = value;
        }
    }
</script>

<style>
    html {
        --dashboard-panel-width: 16%;
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