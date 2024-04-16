using System;
using System.Collections;
using GlobalEnums;
using Modding;
using UnityEngine;
using Satchel;
using Satchel.BetterMenus;


namespace MetalPipeHM;
public class MetalPipeHM : Mod, ICustomMenuMod, ITogglableMod
{
    new public string GetName() => "MetalPipeHM";
    public override string GetVersion() => "v1";

    public bool ToggleButtonInsideMenu => true;

    private Menu MenuRef;

    public MenuScreen GetMenuScreen(MenuScreen modListMenu, ModToggleDelegates? modtoggledelegates)
    {
        MenuRef = new Menu("MetalPipeHM", new Element[]
                {
                    Blueprints.CreateToggle(
                        toggleDelegates: modtoggledelegates.Value,
                        name: "Mod Enabled",
                        description: "Should mod be enabled?"),
                });
        return MenuRef.GetMenuScreen(modListMenu);
    }

    public override void Initialize()
    {
        ModHooks.AttackHook += OnHeroAttack;
        On.HeroController.Awake += OnHeroControllerAwake;
    }

    private void OnHeroControllerAwake(On.HeroController.orig_Awake orig, HeroController self)
    {
        orig.Invoke(self);
        self.gameObject.AddComponent<PipeSoundHandler>();
    }

    private void OnHeroAttack(AttackDirection direction)
    {
        HeroController.instance.GetComponent<PipeSoundHandler>().Run();
    }

    public void Unload()
    {

        if (HeroController.instance != null)
        {
            PipeSoundHandler PipeSoundHandler;
            HeroController.instance.TryGetComponent(out PipeSoundHandler);
            UnityEngine.Object.Destroy(PipeSoundHandler);
        }

        ModHooks.AttackHook -= OnHeroAttack;
        On.HeroController.Awake -= OnHeroControllerAwake;
    }
}