using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using JCotton.DismSharp.Interop;

namespace JCotton.DismSharp {
    public class WindowsFeature {
        private string _featureName;
        private PackageFeatureState _featureState;
        private string _displayName;
        private string _description;
        private RestartType _restartRequired;
        private Dictionary<string, string> _customProperties;

        public string FeatureName => this._featureName;
        public PackageFeatureState FeatureState => this._featureState;
        public string DisplayName => this._displayName;
        public string Description => this._description;
        public RestartType RestartRequired => this._restartRequired;
        public IReadOnlyDictionary<string, string> CustomProperties => this._customProperties;

        public WindowsFeature(DismFeature feature) {
            this._featureName = feature.FeatureName;
            this._featureState = feature.State;
        }

        public WindowsFeature(DismFeatureInfo feature) {
            this._featureName = feature.FeatureName;
            this._featureState = feature.FeatureState;
            this._displayName = feature.DisplayName;
            this._description = feature.Description;
            this._restartRequired = feature.RestartRequired;
            this._customProperties = Utilites.PtrToArray<DismCustomProperty>(
                feature.CustomProperty,
                feature.CustomPropertyCount
                ).ToDictionary(
                    f => string.Format("{0}\\{1}", f.Path, f.Name),
                    f => f.Value
                );
        }
    }
}
