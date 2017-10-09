using Microsoft.Extensions.DependencyInjection;

namespace JCE.Dependency
{
    /// <summary>
    /// 依赖注册管理器
    /// </summary>
    public interface IDependencyRegistrar
    {
        /// <summary>
        /// 注册依赖
        /// </summary>
        /// <param name="services">服务集合</param>
        void Register(IServiceCollection services);
    }
}
