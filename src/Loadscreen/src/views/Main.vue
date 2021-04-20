<template>
    <v-layout fill-height full-width class="ma-0 pa-0" style="overflow: hidden !important;">
        <v-layout row class="loadscreen-bg ma-0 pa-0" fill-height full-width>
            <v-carousel :show-arrows="false"
                        hide-delimiters
                        height="auto"
                        max-height="1080"
                        cycle
                        interval="5000"
                        touchless>
                <v-carousel-item v-for="(image, index) in images"
                                 :key="index"
                                 :src="image.src"
                                 dark
                                 reverse-transition="fade-reverse-transition"
                                 transition="fade-transition"></v-carousel-item>
            </v-carousel>
            <v-card id="log-messages" class="ma-0 pa-1">
                <v-list class="ma-0 pa-0">
                    <v-slide-x-transition>
                        <v-list-item class="ma-0 px-5"
                                     v-for="(log, index) in logMessages"
                                     :key="index">
                            <span class="mr-1">{{ log.eventName }}</span>
                            <span v-if="log.idx !== undefined" class="mr-1">[{{ log.idx }}]</span>
                            <span class="mr-1">{{ log.type }}</span>
                            <span class="mr-1">{{ log.name }}</span>
                            <span>| {{ log.message }}</span>
                        </v-list-item>
                    </v-slide-x-transition>
                </v-list>
            </v-card>
        </v-layout>
        <v-footer app height="auto" class="mx-10 mb-5 pa-0" fixed>
            <v-flex md12 class="ma-0 pa-0">
                <v-progress-linear background-color="secondary"
                                   color="primary"
                                   :value="totalProgress"
                                   height="1.5vw"
                                   reactive
                                   rounded>
                    <span style="font-size: 1vw;"
                          class="white--text">{{logMessage.toUpperCase()}}</span>
                </v-progress-linear>
            </v-flex>
            <v-flex md12 class="ma-0 pa-0">
                <v-card class="mt-1">
                    <v-card-actions>
                        <p class="ma-0">Join us on the forums, discord and twitter for the latest in updates and information!</p>
                        <v-spacer />
                        <v-btn v-for="(action, index) in socialActions" :key="index" text>
                            <v-icon class="mx-1">{{action.icon}}</v-icon>
                            {{action.to}}
                        </v-btn>
                        <v-spacer />
                        <p class="ma-0 caption">&copy; {{(new Date(Date.now())).getFullYear()}} -</p>
                        <p class="ma-0 mx-1 caption">KungRaseri Productions, LLC</p>
                    </v-card-actions>
                </v-card>
            </v-flex>
        </v-footer>
    </v-layout>
</template>

<style lang="scss" scoped>
    .loadscreen-bg {
        background: #212121;
    }

    .carousel-slide-item {
        position: absolute;
        width: 100%;
        height: 100%;
        top: 0;
        left: 0;
        bottom: 0;
        right: 0;
    }

    .fade-transition-enter {
        opacity: 1;
    }

    .fade-transition-enter-active,
    .fade-transition-leave-active {
        transition: all 1.5s ease-in-out;
    }

    .fade-transition-leave-to {
        opacity: 0;
    }
</style>

<script lang="ts">
    import { Component, Vue, Prop } from "vue-property-decorator";

    @Component
    export default class Main extends Vue {
        private socialActions: Record<string, string>[] = [
            {
                to: "redactedgaming.gg",
                icon: "mdi-bulletin-board",
            },
            {
                to: "discord.redactedgaming.gg",
                icon: "mdi-discord",
            },
            {
                to: "twitter.com/RedactedGaming_",
                icon: "mdi-twitter",
            },
        ];
        private images: Record<string, string>[] = [];

        private logMessage = "Initializing...";
        private totalProgress = 0;
        private redactedUrl = "https://redactedgaming.gg";
        public initFunctionInvoked(data: any) {
            this.logMessage = `[Initializing] (${data.type}) | ${data.name}`;
        }
        public initFunctionInvoking(data: any) {
            this.logMessage = `[Initializing] (${data.type}) | ${data.name} | ${data.idx}`;
        }
        public onDataFileEntry(data: any) {
            this.logMessage = `[Initializing] ${data.name}`;
        }
        public endInitFunction() {
            this.logMessage = `[Initialized] Waiting for Session...`;
        }
        public loadProgress(data: any) {
            this.totalProgress = data.loadFraction * 100;
        }
        public onLogLine(data: any) {
            this.logMessage = `[Loading] ${data.message}`;
        }
        public mounted() {
            window.addEventListener("message", (e) => {
                switch (e.data.eventName) {
                    case "initFunctionInvoked":
                        this.initFunctionInvoked(e.data);
                        break;
                    case "initFunctionInvoking":
                        this.initFunctionInvoking(e.data);
                        break;
                    case "onDataFileEntry":
                        this.onDataFileEntry(e.data);
                        break;
                    case "endInitFunction":
                        this.endInitFunction();
                        break;
                    case "onLogLine":
                        this.onLogLine(e.data);
                        break;
                    case "loadProgress":
                        this.loadProgress(e.data);
                        break;
                    default:
                        console.log(e.data.eventName, e.data);
                        break;
                }
            });

            console.log("[OpenRP] Loadscreen has been mounted.");
        }
    }
</script>
