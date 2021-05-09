<template>
    <v-container class="main" v-show="Visible">
        <ChatModule />
        <VehiclePanel />
        <DashboardPanel />
        <CharacterSelect />
        <CharacterCustomizer />
    </v-container>
</template>

<style scoped>
    .home {
        background: none;
    }

    .main {
        position: fixed;
        width: 100%;
        max-width: 10000px;
        height: 100%;
        margin: 0;
        padding: 0;
    }

    .outter {
        max-width: 10000px;
    }
</style>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';
    import ChatModule from '../components/MessageBox/Main.vue';
    import VehiclePanel from '../components/VehiclePanel/Main.vue';
    import DashboardPanel from '../components/VehicleDashboard/Main.vue';
    import CharacterSelect from '../components/CharacterSelect/Main.vue';
    import CharacterCustomizer from '../components/CharacterCustomizer/Main.vue';

    @Component({
        components: {
            ChatModule,
            VehiclePanel,
            DashboardPanel,
            CharacterSelect,
            CharacterCustomizer
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
