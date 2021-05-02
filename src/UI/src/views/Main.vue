<template>
    <v-layout v-show="Visible">
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
    export default class Home extends Vue {
        visible = true;

        mounted() {
            window.addEventListener("message", (e) => {
                switch (e.data.eventName) {
                    case "GET_PAUSED_STATUS":
                        this.Visible = e.data.visible;
                        break;
                    default:
                        break;
                }
            });
        }

        get Visible() {
            return this.visible;
        }

        set Visible(value: boolean) {
            this.visible = value;
        }
    }
</script>
