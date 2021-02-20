using IPA;
using IPA.Config.Stores;
using SiraUtil;
using SiraUtil.Zenject;
using Conf = IPA.Config.Config;
using IPALogger = IPA.Logging.Logger;

namespace VoteSkip
{
    [Plugin(RuntimeOptions.DynamicInit)]
    public class Plugin
    {
        [Init]
        public Plugin(Conf conf, IPALogger logger, Zenjector zenjector)
        {
            Config config = conf.Generated<Config>();
            zenjector.On<PCAppInit>().Pseudo(Container =>
            {
                Container.BindLoggerAsSiraLogger(logger);
                Container.BindInstance(config).AsSingle();
                Container.BindInterfacesAndSelfTo<ChatManager>();
            });

            zenjector.On<MenuInstaller>().Pseudo(Container =>
            {
                Container.BindInterfacesTo<SettingsUI>().AsSingle();
            });

            zenjector.On<GameplayCoreInstaller>().Pseudo(Container =>
            {
                if (config.Enabled)
                {
                    Container.BindInterfacesAndSelfTo<VoteManager>().AsSingle();
                }
            }).OnlyForStandard();
        }

        [OnEnable, OnDisable]
        public void OnState() { /* On State */ }
    }
}