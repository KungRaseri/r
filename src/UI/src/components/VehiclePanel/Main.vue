<template>
    <v-slide-y-reverse-transition>
        <v-card v-show="GetVehiclePanelActive" class="panel" color="rgba(0, 0, 0, 0.6)" rounded="lg">
            <v-container class="inset">
                <v-row class="top-row">
                    <v-col>
                        <v-btn block>
                            Engine
                        </v-btn>
                    </v-col>
                    <v-col>
                        <v-btn block>
                            Belt
                        </v-btn>
                    </v-col>
                </v-row>
                <v-row class="outer-row" dense>
                    <v-col>
                        <v-btn height="100%" block>
                            D
                        </v-btn>
                    </v-col>
                    <v-col>
                        <v-btn height="100%" block>
                            W
                        </v-btn>
                    </v-col>
                    <v-col>
                        <v-btn height="100%" block>
                            S
                        </v-btn>
                    </v-col>
                    <v-col>
                        <v-btn height="100%" block>
                            S
                        </v-btn>
                    </v-col>
                    <v-col>
                        <v-btn height="100%" block>
                            W
                        </v-btn>
                    </v-col>
                    <v-col>
                        <v-btn height="100%" block>
                            D
                        </v-btn>
                    </v-col>
                </v-row>
                <v-row class="outer-row" dense>
                    <v-col>
                        <v-btn height="100%" block>
                            D
                        </v-btn>
                    </v-col>
                    <v-col>
                        <v-btn height="100%" block>
                            W
                        </v-btn>
                    </v-col>
                    <v-col>
                        <v-btn height="100%" block>
                            S
                        </v-btn>
                    </v-col>
                    <v-col>
                        <v-btn height="100%" block>
                            S
                        </v-btn>
                    </v-col>
                    <v-col>
                        <v-btn height="100%" block>
                            W
                        </v-btn>
                    </v-col>
                    <v-col>
                        <v-btn height="100%" block>
                            D
                        </v-btn>
                    </v-col>
                </v-row>
            </v-container>
        </v-card>
    </v-slide-y-reverse-transition>
</template>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';

    @Component({
        components: {
        },
    })
    export default class VehiclePanel extends Vue {
        isVehiclePanelActive = false;

        get GetVehiclePanelActive() {
            return this.isVehiclePanelActive;
        }

        set GetVehiclePanelActive(value: boolean) {
            this.isVehiclePanelActive = value;
        }
    }
</script>

<style>
    html {
        --panel-width: 25%;
    }
</style>

<style scoped>

    .panel {
        position: fixed;
        height: auto;
        width: var(--panel-width);
        bottom: 5%;
        margin-left: calc((100% - var(--panel-width)) / 2 );
        margin-right: calc((100% - var(--panel-width)) / 2);
    }

    .inset {
        height: 100%;
        padding: 10px !important;
    }

    .top-row {
        height: 20%;
        flex-wrap: nowrap;
    }

    .outer-row {
        height: 5vh;
        flex-wrap: nowrap;
    }

    .inner-spacing {
        height: var(--spacer-height);
    }

    .v-btn {
        padding: 0 !important;
    }

    .v-card {
        padding: 0 !important;
    }
</style>